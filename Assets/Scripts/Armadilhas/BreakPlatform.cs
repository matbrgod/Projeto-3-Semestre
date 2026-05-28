using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    float floorTimer = 0f;
    public float maxTimeInFloor;

    float timerFloorAppear;
    public float maxTimeToAppear;

    bool isPLatformBroke = false;
    bool isPlatformTriggered = false;

    public GameObject platform;

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
        if (other.gameObject.CompareTag("Player"))
        {
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
            isPLatformBroke = true;
            isPlatformTriggered = false;
        }
    }

    private void HandlePlatformAppear()
    {
        timerFloorAppear += Time.deltaTime;
        if(timerFloorAppear >= maxTimeToAppear)
        {
            platform.SetActive(true);
            isPLatformBroke = false;
            timerFloorAppear = 0f;
        }
    }
}
