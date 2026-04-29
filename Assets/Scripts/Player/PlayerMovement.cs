using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 playerVel;

    [Header("Refer�ncias")]
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
    public float fallingVel;//passou a ser um multiplicador na intensidade da gravidade
    //em fun��o linear do inAirTimer (quando inAirTimer sobe, ele sobe junto multiplicando
    //a for�a extra vertical)
    public LayerMask groundLayer;

    [Header("Pulo")]
    public int jumpCounter = 0;
    public int maxNumJumps = 2;

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

        // dash
        if (impact.magnitude > 0.2f)
        {
            transform.position += impact * Time.deltaTime;
        }
        impact = Vector3.Lerp(impact, Vector3.zero, drag * Time.deltaTime);

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

        HandleFallAndLand();
        HandleRotation();

        // raycast para detec��o de parede
        if (Physics.SphereCast(raycastOrigin, frontRaycastRadius, Vector3.forward, out hit, raycastMaxDistance, wallLayer))
        {
            moveDirection.x = 0;
            moveDirection.y = 0;
            moveDirection.z = 0;
        }

        //removido a manipula��o do vetor da gravidade manual e substitu�da no fallAndJump pela gravidade
        //da Unity + a for�a extra de gravidade

        //if (isJumping) return;
        //if (dash) return;

        // gravidade para sempre jogar o personagem para baixo quando n�o t� pulando ou dando dash
        //playerVel.y -= fallingVel * 1.5f;

        //playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, fallingVel * 1.5f, playerRb.linearVelocity.z);
        //playerVel.y = 0;
    }

    private void HandleMovement()
    {
        // usa a dire��o da c�mera para determinar a dire��o que o jogador vai andar
        moveDirection = cameraObj.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObj.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        // sprint e andando/correndo (essa varia��o usa o anal�gico do controle)
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

        //Vector3 moveVelocity = new Vector3(moveDirection.x, playerVel.y, moveDirection.z);
        Vector3 moveVelocity = new Vector3(moveDirection.x, playerRb.linearVelocity.y, moveDirection.z);
        if (inputManager.moveInput == Vector3.zero && !isGrounded)
        {
            moveVelocity = new Vector3(0f, playerRb.linearVelocity.y, 0f);
        }
        //reconstru��o parcial da interrup��o do movimento

        playerRb.linearVelocity = moveVelocity;
    }

    private void HandleRotation()
    {
        // rota��o do personagem com a movimenta��o da c�mera
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
        // fun��o para queda do personagem caso ele detecte que n�o tem ch�o abaixo do jogador
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
            //reconstru��o da for�a que aumenta a intensidade da gravidade durante o tempo no ar
            //e aplica��o dela somente ap�s o segundo pulo ou passado determinado tempo caindo
            //playerRb.AddForce(transform.forward * leapingVel);
            //playerRb.AddForce(Vector3.down * fallingVel * inAirTimer);
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
            animManager.animator.SetBool("isJumping", true);
            animManager.PlayTargetAnimation("Jump", false);

            jumpCounter++;

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);

            playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRb.linearVelocity = playerVel;
        }
        else if (jumpCounter < 2 && canDoubleJump) // condicional do pulo duplo
        {
            doubleJump = true;

            animManager.animator.SetBool("isJumping", true);
            animManager.PlayTargetAnimation("Jump", false);

            jumpCounter++;

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * (jumpHeight * 1.5f));
            //alterado o multiplicador de intensidade do segundo pulo para aumentar
            //menos a discrep�ncia de altura entre os dois saltos

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
            if (dashCdTimer >= dashCooldown && !dash) // verifica��o se dash 
            {
                dash = true; // bool para rodar o cooldown do dash

                Vector3 direction = cameraObj.forward; // determina a dire��o do dash sendo a da c�mera
                impact += direction.normalized * dashForce; // dash
            }
        }
    }

    public void HandleDashCd()
    {
        // fun��o para o cooldown do dash
        if (dashCdTimer > 0) // verifica se o timer tem tempo restante, se sim, ele inicia o countdown
        {
            dashCdTimer -= Time.deltaTime;
        }
        if (dashCdTimer <= 0) // verifica se o timer zerou, se sim, ele iguala o timer com o tempo total do cooldown e libera o jogador a usar o dash novamente
        {
            dashCdTimer = dashCooldown;
            dash = false;
        }
    }

    //private void OnDrawGizmos()//gizmo para visualizar as esferas de colis�o
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    //    //Gizmos.DrawWireSphere(transform.position, frontRaycastRadius);
    //}
}
