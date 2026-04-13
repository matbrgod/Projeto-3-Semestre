using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    InputManager input;
    GameObject npcGameObj;

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

    public void HandleInteract()
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
