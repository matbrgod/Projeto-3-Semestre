using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    float floorTimer = 0f;
    public float maxTimeInFloor = 2f;

    float timerFloorAppear;
    public float maxTimeToAppear = 5f;

    public GameObject platform;
    public GameObject lastPlatform;

    bool isPLatformBroke = false;

    private void Update()
    {
        //Debug.Log($"floorTimer tem {floorTimer} segundos");
        if (isPLatformBroke)
        {
            HandlePlatformAppear();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ChaoFalso"))
        {
            platform = other.gameObject;
            HandlePlatformBreak();
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("ChaoTemp"))
    //    {
    //        plataforma = collision.gameObject;
    //        HandlePlatformBreak();
    //    }
    //}

    private void HandlePlatformBreak()
    {
        //Debug.Log("Chamou HandlePlatformBreak");
        floorTimer += Time.deltaTime;
        if (floorTimer >= maxTimeInFloor)
        {
            //Debug.Log("Chamou a condicionado do handle");
            platform.SetActive(false);
            lastPlatform = platform;
            platform = null;
            floorTimer = 0f;
            isPLatformBroke = true;
        }
    }

    private void HandlePlatformAppear()
    {
        timerFloorAppear += Time.deltaTime;
        if(timerFloorAppear >= maxTimeToAppear)
        {
            lastPlatform.SetActive(true);
            timerFloorAppear = 0f;
            isPLatformBroke = false;
        }
    }
}
