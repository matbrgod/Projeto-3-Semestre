using UnityEngine;

public class Interagir : MonoBehaviour
{
        [SerializeField] Transform cameraTransform;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, 100f))
        {
            Debug.Log("Objeto Interagível: " + hitInfo.collider.gameObject.name);
            // Aqui você pode adicionar a lógica para interagir com o objeto
        }
    }
}
