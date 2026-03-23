using UnityEngine;
using UnityEngine.InputSystem;

public class Tiro : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] Transform Muzzle;
    [SerializeField] float speed;

    public void OnTiro(InputValue valor)
    {
        if (valor.isPressed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, Muzzle.position, Muzzle.rotation);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        
        if (rb != null)
            rb.linearVelocity = Muzzle.forward * speed;
    }
}
