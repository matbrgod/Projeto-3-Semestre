using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    float floorTimer = 0f;
    public float maxTimeInFloor = 2f;

    GameObject plataforma;

    private void Update()
    {
        if(floorTimer >= maxTimeInFloor)
        {
            Destroy(plataforma.transform.parent);
            plataforma = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChaoFalso"))
        {
            floorTimer += floorTimer + Time.deltaTime;
            plataforma = other.gameObject;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("ChaoFalso"))
    //    {
    //        floorTimer = 0;
    //        plataforma = null;
    //    }
    //}
}
