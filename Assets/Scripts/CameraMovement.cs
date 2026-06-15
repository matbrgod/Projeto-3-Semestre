using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform currentTarget;
    public float followSpeed = 5f;
    InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
       
    }
    void LateUpdate()
    {
        if (currentTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, currentTarget.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentTarget.rotation, followSpeed * Time.deltaTime);
        }
    }
    void ChangeTarget(Transform newTarget)
        {
        currentTarget = newTarget;
        }
}
