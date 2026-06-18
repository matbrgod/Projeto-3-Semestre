using UnityEngine;

public class CameraCutscene : MonoBehaviour
{
    public Transform currentTarget;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        if (currentTarget != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            transform.position = Vector3.Lerp(transform.position, currentTarget.position, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentTarget.rotation, speed);
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
