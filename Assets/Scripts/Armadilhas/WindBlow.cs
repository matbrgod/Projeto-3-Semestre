using UnityEngine;

public class WindBlow : MonoBehaviour
{
    GameObject wind;
    Rigidbody playerRb;

    public float windForce;

    Vector3 impact;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Vento"))
        {
            wind = other.gameObject;
            Vector3 direction = -wind.transform.up;
            impact += direction.normalized * windForce;
            //transform.position += impact * Time.deltaTime;
            playerRb.AddForce(impact, ForceMode.Force);
        }
    }
}
