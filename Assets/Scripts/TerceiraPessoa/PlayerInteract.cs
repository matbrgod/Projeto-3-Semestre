using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    InputManager input;

    public GameObject npcGameObj;
    public GameObject dialogBox;

    public bool canInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            npcGameObj = other.gameObject;
            canInteract = true;
        }
    }

    public void HandleNpcInteract()
    {
        if (npcGameObj != null)
        {
            npcGameObj.GetComponentInParent<NpcActions>().Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            npcGameObj = null;
            canInteract = false;
        }
    }
}
