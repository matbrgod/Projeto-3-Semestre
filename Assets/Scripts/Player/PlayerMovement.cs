using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 playerVel;

    [Header("Referências")]
    public Transform orientation;
    Transform cameraObj;
    Rigidbody playerRb;
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animManager;

    [Header("Flag de Movimento")]
    public bool isGrounded;
    public bool isJumping;
    public bool isSprinting;
    public bool dash;
    public bool doubleJump;
    public bool canDoubleJump;
    public bool canDash;

    [Header("Queda")]
    public float inAirTimer;
    public float leapingVel;
    public float fallingVel;
    public LayerMask groundLayer;

    [Header("Pulo")]
    public int jumpCounter = 0;
    public int maxNumJumps = 2;

    [Header("Dash")]
    public float dashForce;
    public float drag = 5f;
    public float dashCooldown;
    private float dashCdTimer; // cooldown timer
    private Vector3 impact;

    [Header("Raycast")]
    public float raycastHeightOffSet = 0.5f;
    public float raycastRadius = 0.2f;
    public float spherecastMaxDistance = 0.5f; // distância pra queda
    public float raycastMaxDistance = 0.5f; // distância pra andar
    public float frontRaycastRadius = 0.2f;
    public LayerMask wallLayer;

    [Header("Velocidade de Movimento")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Velocidade de Pulo e Gravidade")]
    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        animManager = GetComponent<AnimatorManager>();
        playerRb = GetComponent<Rigidbody>();

        cameraObj = Camera.main.transform;

        dashCdTimer = dashCooldown;

        canDash = true;
        canDoubleJump = true;

        isGrounded = true;
    }

    public void HandleMoves()
    {
        RaycastHit hitFloor;
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;

        if (impact.magnitude > 0.2f)
        {
            transform.position += impact * Time.deltaTime;
        }
        impact = Vector3.Lerp(impact, Vector3.zero, drag * Time.deltaTime);

        HandleMovement();

        if (Physics.Raycast(raycastOrigin, -Vector3.up, out hitFloor, raycastMaxDistance))
        {
            Debug.DrawLine(transform.position, hitFloor.collider.transform.position);
            if (hitFloor.distance < 0.46 && !isJumping && !playerManager.isInteracting)
            {
                isGrounded = true;
            }
        }

        HandleFallAndLand();
        HandleRotation();

        if (Physics.SphereCast(raycastOrigin, frontRaycastRadius, Vector3.forward, out hit, raycastMaxDistance, wallLayer))
        {
            moveDirection.x = 0;
            moveDirection.y = 0;
            moveDirection.z = 0;
        }

        if(dash) HandleDashCd();

        if (isJumping) return;
        if (dash) return;

        playerVel.y -= fallingVel * 1.5f;
        playerVel.y = 0;
    }

    private void HandleMovement()
    {
        moveDirection = cameraObj.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObj.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
        }
        else
        {
            if (inputManager.moveAmout >= 0.5f)
            {
                moveDirection = moveDirection * runSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
            }
        }

        Vector3 moveVelocity = new Vector3(moveDirection.x, playerVel.y, moveDirection.z);
        playerRb.linearVelocity = moveVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping || doubleJump) return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObj.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObj.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallAndLand()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffSet;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            playerRb.AddForce(transform.forward * leapingVel);
            playerRb.AddForce(Vector3.down * fallingVel * inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, raycastRadius, Vector3.down, out hit, spherecastMaxDistance, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
            jumpCounter = 0;
            doubleJump = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            animManager.animator.SetBool("isJumping", true);
            animManager.PlayTargetAnimation("Jump", false);

            jumpCounter++;

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRb.linearVelocity = playerVel;
        }
        else if (jumpCounter < 2 && canDoubleJump)
        {
            doubleJump = true;

            animManager.animator.SetBool("isJumping", true);
            animManager.PlayTargetAnimation("Jump", false);

            jumpCounter++;

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * (jumpHeight * 2f));
            playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRb.linearVelocity = playerVel;
        }
    }

    public void HandleDash()
    {
        if (canDash) 
        {
            if (dashCdTimer >= dashCooldown && !dash)
            {
                dash = true;

                Vector3 direction = transform.forward;
                impact += direction.normalized * dashForce;
            }
        }
    }

    public void HandleDashCd()
    {
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
        if(dashCdTimer <= 0)
        {
            dashCdTimer = dashCooldown;
            dash = false;
        }
    }
}
