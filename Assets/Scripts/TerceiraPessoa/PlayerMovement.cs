using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 playerVel;

    Transform cameraObj;
    Rigidbody playerRb;
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animManager;

    [Header("Flag de Movimento")]
    public bool isGrounded;
    public bool isJumping;

    [Header("Queda")]
    public float inAirTimer;
    public float leapingVel;
    public float fallingVel;
    public float raycastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header("Velocidade de Movimento")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Velocidade de Pulo e Gravidade")]
    public float jumpHeight = 3f;
    public float gravityIntensity = 15f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        animManager = GetComponent<AnimatorManager>();
        playerRb = GetComponent<Rigidbody>();

        cameraObj = Camera.main.transform;

        isGrounded = true;
    }

    public void HandleMoves()
    {
        HandleFallAndLand();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObj.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObj.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        if (inputManager.moveAmout >= 0.5f)
        {
            moveDirection = moveDirection * runSpeed;
        }
        else
        {
            moveDirection = moveDirection * walkSpeed;
        }

        Vector3 moveVelocity = new Vector3(moveDirection.x, playerVel.y, moveDirection.z);
        playerRb.linearVelocity = moveVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping) return;

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
            playerVel.y -= fallingVel;
            playerVel.y = 0;
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out hit, 0.5f, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
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

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRb.linearVelocity = playerVel;
        }
    }
}
