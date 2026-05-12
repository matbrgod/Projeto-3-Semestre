using UnityEngine;

public class WindChange : MonoBehaviour
{
    public float windForce;
    public float time = 3.0f;
    public float repeatTime = 3.0f;
    public bool isLeft = true;

    public Vector3 direction = Vector3.forward;
    public Vector3 impact;

    void Start()
    {
        InvokeRepeating(nameof(HandleHorizontalWindChange), time, repeatTime);
    }

    private void FixedUpdate()
    {
        impact += direction * windForce;
    }

    private void HandleHorizontalWindChange()
    {
        if (isLeft)
        {
            direction = Vector3.forward;
            impact = new Vector3(0f, 0f, 0f);
            isLeft = false;
        }
        else
        {
            direction = Vector3.back;
            impact = new Vector3(0f, 0f, 0f);
            isLeft = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(impact, ForceMode.Force);
        }
    }
}
