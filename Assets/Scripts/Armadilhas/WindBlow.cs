using UnityEngine;

public class WindBlow : MonoBehaviour
{public float windForce;

    public Vector3 direction = Vector3.forward;
    Vector3 impact;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.attachedRigidbody != null)
        {
            //wind = other.gameObject;
            impact += direction.normalized * windForce;
            other.attachedRigidbody.AddForce(impact, ForceMode.Force);
        }
    }
}
