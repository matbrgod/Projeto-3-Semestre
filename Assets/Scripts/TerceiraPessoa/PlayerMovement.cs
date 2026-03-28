using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;

    Transform cameraObj;
    InputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    public void HandleMovement()
    {
        moveDirection = cameraObj.forward * inputManager.verticalInput;
    }
}
