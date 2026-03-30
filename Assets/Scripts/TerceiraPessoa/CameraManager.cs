using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;
    public Transform camPivot;
    public Transform camTransform;

    public float followSpeed;
    public float camLookSpeed = 0.5f;
    public float camPivotSpeed = 2f;
    public float lookAngle;
    public float pivotAngle;
    public float minPivotAngle = -40;
    public float maxPivotAngle = 55;

    private Vector3 cameraFollowVel = Vector3.zero;

    private float defaultCamPos;

    private void Awake()
    {
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
        inputManager = FindFirstObjectByType<InputManager>();

        camTransform = Camera.main.transform;
    }

    public void HandleCamMove()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVel, followSpeed);
        transform.position = targetPos;
    }

    private void RotateCamera()
    {
        Quaternion targerRotation;
        Vector3 rotation;

        lookAngle = lookAngle + (inputManager.camXInput * camLookSpeed);
        pivotAngle = pivotAngle - (inputManager.camYInput * camPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targerRotation = Quaternion.Euler(rotation);
        transform.rotation = targerRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targerRotation = Quaternion.Euler(rotation);
        camPivot.localRotation = targerRotation;
    }

    private void HandleCamCollision()
    {
        float targetPos = defaultCamPos;
    }
}
