using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player3rdPersonControl playerControl;

    public Vector2 moveInput;

    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new Player3rdPersonControl();

            playerControl.PlayerMove.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
        }

        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = moveInput.y;
        horizontalInput = moveInput.x;
    }
}
