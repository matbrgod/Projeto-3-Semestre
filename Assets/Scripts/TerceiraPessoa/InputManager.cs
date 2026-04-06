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

            // detecta o input de pulo
            playerControl.PlayerActions.Jump.performed += i => jumpInput = true;

            // detecta o input do esc
            playerControl.PlayerActions.Escape.performed += i => escInput = true;
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
    }

    private void HandleMovementInput()
    {
        verticalInput = moveInput.y;
        horizontalInput = moveInput.x;

        moveAmout = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animManager.UpdateAnimatorValues(0, moveAmout);

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
}
