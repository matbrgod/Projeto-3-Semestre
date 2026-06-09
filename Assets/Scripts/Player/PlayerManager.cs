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
    }

    void Start()
    {
        
    }

    void Update()
    {
        inputManager.HandleInputs(); // detecta os inputs do jogador
    }

    private void FixedUpdate()
    {
        playerMove.HandleMoves(); // detecta a movimentação
    }

    private void LateUpdate()
    {
        camManager.HandleCamMove(); // detecta a movimentação da câmera

        // set dos bools do animator
        isInteracting = animator.GetBool("isInteracting");

        playerMove.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMove.isGrounded);
    }
}
