using UnityEngine;

public class WindBlow : MonoBehaviour
{
    public float windForce;
    public Vector3 direction = Vector3.forward;
    Vector3 impact;

    private void FixedUpdate()
    {
        impact += direction * windForce;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(impact, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        direction = Vector3.zero;
    }
}
