using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class WindChange : MonoBehaviour
{
    public float windForce;
    public float time = 3.0f;
    public float repeatTime = 3.0f;
    public bool isLeft = true;

    public Vector3 direction = Vector3.forward;
    Vector3 impact;

    void Start()
    {
        InvokeRepeating(nameof(HandleHorizontalWindChange), time, repeatTime);
    }

    private void HandleHorizontalWindChange()
    {
        if (isLeft)
        {
            direction = Vector3.forward;
            isLeft = false;
        }
        else
        {
            direction = -Vector3.forward;
            isLeft = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.attachedRigidbody != null)
        {
            impact += direction.normalized * windForce;
            other.attachedRigidbody.AddForce(impact, ForceMode.Force);
        }
    }
}
