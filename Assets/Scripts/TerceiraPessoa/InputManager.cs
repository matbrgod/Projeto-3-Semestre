using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputManager : MonoBehaviour
{
    public PlayerInputSystem playerControl;
    AnimatorManager animManager;
    PlayerMovement playerMove;

    public Vector3 moveInput;
    public Vector2 camInput;

    public float verticalInput;
    public float horizontalInput;
    public float camXInput;
    public float camYInput;
    public float moveAmout;

    public bool jumpInput;
    public bool escInput;
    public bool sprintInput;

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
            playerControl.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControl.PlayerActions.Sprint.canceled += i => sprintInput = false;

            playerControl.PlayerActions.Jump.performed += i => jumpInput = true;

            playerControl.PlayerActions.Pause.performed += i => escInput = true;
        }

        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void HandleInputs()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleSprintInput();
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
}
