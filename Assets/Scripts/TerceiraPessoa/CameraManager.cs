using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 cameraFollowVel = Vector3.zero;

    public float followSpeed = 0.2f;
    public float lookAngle;
    public float pivotAngle;

    private void Awake()
    {
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
    }

    public void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVel, followSpeed);
        transform.position = targetPos;
    }

    public void RotateCamera()
    {
        //lookAngle = lookAngle + (mouseXInput * camLookSpeed);
        //pivotAngle = pivotAngle - (mouseYInput * camPivotSpeed);
    }
}
