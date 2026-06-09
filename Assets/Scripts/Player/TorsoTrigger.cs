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
        if(!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida"))
            isTorsoTriggered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida"))
            isTorsoTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida"))
            isTorsoTriggered = false;
    }
}
