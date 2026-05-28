using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float look_Sensitivity;
    public Transform cameraTransform;
    float pitch;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    
    void Update()
    {
        MouseMovement();
    }

    void MouseMovement ()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float yaw = mouseDelta.x * look_Sensitivity;
        transform.Rotate(Vector3.up * yaw);

        pitch -= mouseDelta.y * look_Sensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
