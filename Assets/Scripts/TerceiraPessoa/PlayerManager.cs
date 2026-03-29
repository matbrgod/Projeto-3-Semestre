using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMove;
    CameraManager camManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMove = GetComponent<PlayerMovement>();
        camManager = FindFirstObjectByType<CameraManager>();
    }

    void Start()
    {
        
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
    }
}
