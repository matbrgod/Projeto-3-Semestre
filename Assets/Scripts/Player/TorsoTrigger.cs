using UnityEngine;

public class TorsoTrigger : MonoBehaviour
{
    public static TorsoTrigger instance;
    PlayerMovement playerMove;

    public bool isTorsoTriggered;

    private void Awake()
    {
        instance = this;
        playerMove = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        isTorsoTriggered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isTorsoTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTorsoTriggered = false;
    }
}
