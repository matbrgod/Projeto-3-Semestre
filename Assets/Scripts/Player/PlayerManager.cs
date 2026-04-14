using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    PlayerMovement playerMove;
    CameraManager camManager;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMove = GetComponent<PlayerMovement>();
        camManager = FindFirstObjectByType<CameraManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        inputManager.HandleInputs();
    }

    private void FixedUpdate()
    {
        playerMove.HandleMoves();
    }

    private void LateUpdate()
    {
        camManager.HandleCamMove();

        isInteracting = animator.GetBool("isInteracting");

        playerMove.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMove.isGrounded);
    }
}
