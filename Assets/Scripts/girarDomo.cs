using UnityEngine;

public class girarDomo : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50f; // degrees per second

    private float rotationZ = 0f;

    void Update()
    {
        rotationZ += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-90f, 0, rotationZ);
    }
}
