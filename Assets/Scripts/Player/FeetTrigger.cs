using UnityEngine;

public class FeetTrigger : MonoBehaviour
{
    public static FeetTrigger instance;
    PlayerMovement playerMove;
    GameObject collisionObj;

    public bool isFeetTriggered;

    

    private void Awake()
    {
        instance = this;
        playerMove = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (isFeetTriggered)
        {
            float upWeight = Vector3.Dot(collisionObj.transform.position, collisionObj.transform.up);
            float forwardWeight = Vector3.Dot(collisionObj.transform.position, collisionObj.transform.forward);
            float rightWeight = Vector3.Dot(collisionObj.transform.position, collisionObj.transform.right);

            playerMove.enabled = false;
            if (collisionObj != null) transform.parent.position += collisionObj.GetComponent<BoxCollider>().center + (collisionObj.transform.up * 0.5f);
            playerMove.enabled = true;

            isFeetTriggered = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida") && !playerMove.isGrounded)
        {
            isFeetTriggered = true;
            collisionObj = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida") && !playerMove.isGrounded)
            isFeetTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Triggers") && !other.CompareTag("Checkpoint") && !other.CompareTag("MiniShrine") && !other.CompareTag("PedraJapao") && !other.CompareTag("MiniShrineInteragida") && !playerMove.isGrounded) 
        { 
            isFeetTriggered = false;
            collisionObj = null;
        }
    }
}
