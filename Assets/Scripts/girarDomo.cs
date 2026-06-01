using NUnit.Framework.Constraints;
using UnityEngine;

public class girarDomo : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50f; // degrees per second

    private float rotationZ = 0f;
    public bool invertRotation = true;

    void Start()
    {
        rotationZ += rotationSpeed * Time.deltaTime;
        
    }
    void Update()
    {
        if (invertRotation)
        {
            rotationZ = -rotationZ;
            transform.rotation = Quaternion.Euler(-90f, 0, -rotationZ);
        }
        else
        {
           
            transform.rotation = Quaternion.Euler(-90f, 0, rotationZ);
        }
    }
}
