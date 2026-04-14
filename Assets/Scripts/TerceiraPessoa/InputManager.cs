using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputManager : MonoBehaviour
{
    public PlayerInputSystem playerControl;
    AnimatorManager animManager;
    PlayerMovement playerMove;
    PlayerInteract playerInteract;

    public Vector3 moveInput;
    public Vector2 camInput;

    public float verticalInput;
    public float horizontalInput;
    public float camXInput;
    public float camYInput;
    public float moveAmout;

    // variáveis dos inputs de interação do jogador
    public bool jumpInput;
    public bool sprintInput;
    public bool dashInput;
    public bool pauseInput;
    public bool interactInput;

    private void Awake()
    {
        animManager = GetComponent<AnimatorManager>();
        playerMove = GetComponent<PlayerMovement>();
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
            playerControl.PlayerActions.Jump.performed += i => jumpInput = true;

            playerControl.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControl.PlayerActions.Sprint.canceled += i => sprintInput = false;

            playerControl.PlayerActions.Dash.performed += i => dashInput = true;

            playerControl.PlayerActions.Pause.performed += i => pauseInput = true;

            playerControl.PlayerActions.Interact.performed += i => interactInput = true;
        }

        playerControl.Enable();
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
            //playerMove.HandleDash();
            dashInput = false;
        }
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            Debug.Log("A tecla de input foi pressionada!");
            //if(playerInteract.npcGameObj != null)
            //{
            //    playerInteract.HandleNpcInteract();
            //}
            interactInput = false;
        }
    }
}
