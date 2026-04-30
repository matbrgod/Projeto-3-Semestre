using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // código de diálogo do gilles que eu preciso rever

    [Header("Refs")]
    InputManager input;
    //public GameObject npcGameObj; // npc
    public GameObject shrineObj; // mini templo

    [Header("Flags de Interação")]
    public bool canInteract = false;
    public bool miniShrine = false;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI shrineCounterTxt; // contador de conhecimento
    [SerializeField] GameObject shrineCounterUi; // UI do contador de conhecimento
    //public GameObject dialogBox; // caixa de diálogo do npc

    int shrineCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("NPC"))
        //{
        //    other.transform.GetChild(0).gameObject.SetActive(true);
        //    npcGameObj = other.gameObject;
        //    canInteract = true;
        //}

        if (other.gameObject.CompareTag("MiniShrine"))
        {
            shrineObj = other.gameObject;
            miniShrine = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            miniShrine = false;
            shrineObj = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("NPC"))
        //{
        //    other.transform.GetChild(0).gameObject.SetActive(false);
        //    npcGameObj = null;
        //    canInteract = false;
        //}

        if (other.gameObject.CompareTag("MiniShrine"))
        {
            shrineObj = null;
            miniShrine = false;
        }
    }

    //public void HandleNpcInteract()
    //{
    //    if (npcGameObj != null)
    //    {
    //        npcGameObj.GetComponentInParent<NpcActions>().Interact();
    //    }
    //}

    public void MiniShrineInteract()
    {
        if(shrineObj != null)
        {
            shrineCounterUi.SetActive(true);
            shrineCounter++;
            shrineCounterTxt.text = shrineCounter.ToString();
            shrineObj.tag = "Untagged";
        }
    }
}
