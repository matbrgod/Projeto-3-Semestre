using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 playerVel;

    [Header("Refer�ncias")]
    public Transform cameraObj;
    Rigidbody playerRb;
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animManager;
    AudioManager audioManager;

    [Header("Flag de Movimento")]
    public bool isGrounded;
    public bool isJumping;
    public bool isSprinting;
    public bool isWalking;
    public bool isLanding;
    public bool canJump;
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
    public float jumpCooldown = 2f;
    private float jumpCdTimer;

    [Header("Dash")]
    public float dashForce;
    public float drag = 5f;
    public float dashCooldown; // valor base do cooldown
    private float dashCdTimer; // cooldown timer
    private Vector3 impact;

    [Header("Raycast")]
    public float raycastHeightOffSet = 0.5f;
    public float sphereCastRadius = 0.2f;
    public float spherecastMaxDistance = 0.5f; // dist�ncia que verifica pra queda
    public float raycastMaxDistance = 0.5f; // dist�ncia que verifica pra andar (tentativa de mexer no bug da anima��o)
    public float frontRaycastRadius = 0.2f; // raio do raycast da colis�o com paredes
    public LayerMask wallLayer;
    public PhysicsMaterial noFrictionMat; // material f�sico sem atrito para aplicar nas paredes

    [Header("Velocidade de Movimento")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;
    [HideInInspector] public float moveSpeed;
    private float playerWalkSpeed; // velocidade base do jogador andando
    private float playerRunSpeed; // velocidade base do jogador correndo
    private float playerSprintSpeed; // velocidade base do jogador usando sprint

    [Header("Velocidade de Pulo e Gravidade")]
    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    IEnumerator stepsSfx;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        animManager = GetComponent<AnimatorManager>();
        playerRb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        stepsSfx = audioManager.HandleSteps();

        playerWalkSpeed = walkSpeed;
        playerRunSpeed = runSpeed;
        playerSprintSpeed = sprintSpeed;

        cameraObj = Camera.main.transform;

        dashCdTimer = dashCooldown;
        jumpCdTimer = jumpCooldown;

        canJump = true;
        //canDash = true;
        canDoubleJump = true;

        isGrounded = true;
    }

    public void HandleMoves()
    {
        RaycastHit hitFloor;
        Vector3 raycastOrigin = transform.position;

        isLanding = playerManager.isInteracting;

        if(isJumping || isLanding || !isGrounded || PlayerRespawner.instance.isRespawning)
            isWalking = false;
        else if (!isJumping && !isLanding && isGrounded && !PlayerRespawner.instance.isRespawning) isWalking = inputManager.playerControl.PlayerMove.Movement.IsPressed();

        // dash
        if (impact.magnitude > 0.2f)
        {
            transform.position += impact * Time.deltaTime;
        }
        impact = Vector3.Lerp(impact, Vector3.zero, drag * Time.deltaTime);

        if(!isWalking && !isJumping && PlayerRespawner.instance.isRespawning)
        {
            inputManager.moveAmount = 0;
            inputManager.verticalInput = 0;
            inputManager.horizontalInput = 0;
            animManager.animator.SetBool("isIdle", true);
        }

        if (PlayerRespawner.instance.isRespawning)
        {
            inputManager.moveAmount = 0;
            inputManager.verticalInput = 0;
            inputManager.horizontalInput = 0;
            animManager.animator.SetBool("isIdle", true);
            playerRb.linearVelocity = Vector3.zero;
        }

        HandleMovement();

        if (Physics.Raycast(raycastOrigin, -Vector3.up, out hitFloor, raycastMaxDistance))
        {
            if (hitFloor.distance < 0.46 && !isJumping && !playerManager.isInteracting)
            {
                isGrounded = true;
            }
        }

        // cooldown do dash
        if (dash) HandleDashCd();
        if (!canJump) HandleJumpCd();

        HandleFallAndLand();
        HandleRotation();

        // raycast para deteccao de parede
        if (TorsoTrigger.instance.isTorsoTriggered)
        {
            inputManager.moveInput = Vector3.zero;
            playerRb.AddForce(Physics.gravity * inAirTimer * fallingVel);
        }
    }

    private void HandleMovement()
    {
        // usa a direcao da camera para determinar a direcao que o jogador vai andar
        moveDirection = cameraObj.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObj.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        // sprint e andando/correndo (essa varia��o usa o anal�gico do controle)
        if (isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
            moveSpeed = 0.2f;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runSpeed;
                moveSpeed = 0.4f;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
                moveSpeed = 0.75f;
            }
        }

        Vector3 moveVelocity = new Vector3(moveDirection.x, playerRb.linearVelocity.y, moveDirection.z);
        if (inputManager.moveInput == Vector3.zero && !isGrounded)
        {
            moveVelocity = new Vector3(0f, playerRb.linearVelocity.y, 0f);
        }
        //reconstru��o parcial da interrup��o do movimento

        playerRb.linearVelocity = moveVelocity;
        if (audioManager != null)
        {
            if (isWalking && !audioManager.waitForStep)
            {
                StartCoroutine(stepsSfx);
            }
            else if (!isWalking)
            {
                audioManager.waitForStep = false;
                StopCoroutine(stepsSfx);
            }
        }
    }

    private void HandleRotation()
    {
        // rotacao do personagem com a movimentacao da camera
        if (isJumping || doubleJump) return; // impeditivo do personagem rodar caso ele esteja pulando e o jogador mova a c�mera

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObj.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObj.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallAndLand()
    {
        // funcao para queda do personagem caso ele detecte que nao tem chao abaixo do jogador
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffSet;

        // queda
        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            if (jumpCounter > 1 || inAirTimer > 0.3f)
            {
                playerRb.AddForce(Physics.gravity * inAirTimer * fallingVel);
            }
        }

        // pouso
        if (Physics.SphereCast(raycastOrigin, sphereCastRadius, Vector3.down, out hit, spherecastMaxDistance, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                if (audioManager != null && !isJumping) audioManager.PlaySfx(audioManager.landSfx);
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
        if (isGrounded) // condicional do pulo simples
        {
            if (canJump)
            {
                isWalking = false;
                canJump = false;
                moveSpeed = 0;

                animManager.animator.SetBool("isJumping", true);
                animManager.PlayTargetAnimation("Jump", false);

                jumpCounter++;

                float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);

                playerVel = moveDirection;
                playerVel.y = jumpingVel;
                playerRb.linearVelocity = playerVel;
                if (audioManager != null) audioManager.PlaySfx(audioManager.jumpSfx);
            }
        }
        else if (jumpCounter < 2 && canDoubleJump) // condicional do pulo duplo
        {
            doubleJump = true;
            moveSpeed = 0;

            animManager.animator.SetBool("isJumping", true);
            animManager.PlayTargetAnimation("Jump", false);

            jumpCounter++;

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * (jumpHeight * 1.5f));

            playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRb.linearVelocity = playerVel;
        }
    }

    public void HandleDash()
    {
        // fun��o de dash
        if (canDash) // verifica��o se o jogador pode usar o dash
        {
            if (dashCdTimer >= dashCooldown && !dash) // verificacao se dash 
            {
                dash = true; // bool para rodar o cooldown do dash

                Vector3 direction = cameraObj.forward; // determina a direcao do dash sendo a da camera
                impact += direction.normalized * dashForce; // dash
            }
        }
    }

    public void HandleDashCd()
    {
        // funcao para o cooldown do dash
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
        if (dashCdTimer <= 0)
        {
            dashCdTimer = dashCooldown;
            dash = false;
        }
    }

    public void HandleJumpCd()
    {
        // funcao de cooldown para o pulo
        if(jumpCdTimer > 0)
            jumpCdTimer -= Time.deltaTime;
        else if (jumpCdTimer <= 0)
        {
            jumpCdTimer = jumpCooldown;
            canJump = true;
        }
    }
}