using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputManager : MonoBehaviour
{
    [Header("Refs")]
    public PlayerInputSystem playerControl;
    AnimatorManager animManager;
    PlayerMovement playerMove;
    PlayerRespawn playerRespawn;
    PlayerInteract playerInteract;

    [Header("Vetores dos Inputs")]
    public Vector3 moveInput;
    public Vector2 camInput;

    [Header("Valores dos Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public float camXInput;
    public float camYInput;
    public float moveAmout;

    [Header("Flags dos Inputs")]
    public bool jumpInput;
    public bool sprintInput;
    public bool dashInput;
    public bool pauseInput;
    public bool interactInput;

    bool isPaused;

    private void Awake()
    {
        animManager = GetComponent<AnimatorManager>();
        playerMove = GetComponent<PlayerMovement>();

        isPaused = false;
    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerInputSystem();

            // detecta os inputs de movimento e da camera (respectivamente)
            playerControl.PlayerMove.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
            playerControl.PlayerMove.Camera.performed += i => camInput = i.ReadValue<Vector2>();

            // detecta os inputs que o jogador fizer
            playerControl.PlayerActions.Jump.performed += i => jumpInput = true; // input de pulo

            // input do sprint
            playerControl.PlayerActions.Sprint.performed += i => sprintInput = true; // detecta se o botão foi pressionado
            playerControl.PlayerActions.Sprint.canceled += i => sprintInput = false; // detecta se ele deixou de ser pressionado

            playerControl.PlayerActions.Dash.performed += i => dashInput = true; // input do dash

            playerControl.PlayerActions.Pause.performed += i => pauseInput = true; // input de pause

            playerControl.PlayerActions.Interact.performed += i => interactInput = true; // input de interação
        }

        playerControl.Enable(); // habilita o input system do jogador
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void HandleInputs()
    {
        // inputs de movimento
        HandleMovementInput();
        HandleJumpInput();
        HandleSprintInput();
        HandleDashInput();

        //inputs diversos
        HandlePauseInput();
        HandleInteractInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = moveInput.y;
        horizontalInput = moveInput.x;

        moveAmout = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animManager.UpdateAnimatorValues(0, moveAmout, playerMove.isSprinting);

        camYInput = camInput.y;
        camXInput = camInput.x;
    }

    private void HandleJumpInput()
    {
        if(jumpInput == true)
        {
            playerMove.HandleJump();
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                playerMove.HandleJump();
            jumpInput = false;
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmout > 0.5f)
        {
            playerMove.isSprinting = true;
        }
        else
        {
            playerMove.isSprinting = false;
        }
    }

    private void HandleDashInput()
    {
        if (dashInput)
        {
            playerMove.HandleDash();
            dashInput = false;
        }
    }

    private void HandlePauseInput()
    {
        if (pauseInput && !isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(pauseInput && isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            if (playerInteract.miniShrine)
            {
                playerInteract.MiniShrineInteract();
            }
            interactInput = false;
        }
    }
}
