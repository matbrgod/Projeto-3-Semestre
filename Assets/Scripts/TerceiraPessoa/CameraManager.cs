using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    public Transform targetTransform;
    public Transform camPivot;

    private Vector3 cameraFollowVel = Vector3.zero;

    public float followSpeed = 0.2f;
    public float camLookSpeed = 2f;
    public float camPivotSpeed = 2f;
    public float lookAngle;
    public float pivotAngle;
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;

    private void Awake()
    {
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
        inputManager = FindFirstObjectByType<InputManager>();
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
        lookAngle = lookAngle + (inputManager.camXInput * camLookSpeed);
        pivotAngle = pivotAngle - (inputManager.camYInput * camPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targerRotation = Quaternion.Euler(rotation);
        transform.rotation = targerRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targerRotation = Quaternion.Euler(rotation);
        camPivot.localRotation = targerRotation;
    }
}
