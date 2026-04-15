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
    bool isPlatformTriggered = false;

    private void Update()
    {
        if (isPLatformBroke)
        {
            HandlePlatformAppear();
        }
        if (isPlatformTriggered)
        {
            HandlePlatformBreak();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChaoFalso"))
        {
            platform = other.gameObject;
            isPlatformTriggered = true;
        }
    }

    private void HandlePlatformBreak()
    {
        floorTimer += Time.deltaTime;
        if (floorTimer >= maxTimeInFloor)
        {
            floorTimer = 0f;
            platform.SetActive(false);
            lastPlatform = platform;
            platform = null;
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
