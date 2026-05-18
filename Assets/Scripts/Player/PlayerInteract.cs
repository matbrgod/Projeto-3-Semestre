using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Refs")]
    InputManager input;
    DialogueManager dialogueManager;
    public GameObject stoneGameObj;
    public GameObject shrineObj; // mini templo

    [Header("Flags de Interação")]
    public bool canInteract = false;
    public bool miniShrine = false;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI shrineCounterTxt; // contador de conhecimento
    [SerializeField] GameObject shrineCounterUi; // UI do contador de conhecimento

    public int shrineCounter = 0;

    private void Start()
    {
        //dialogueManager = stoneGameObj.GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PedraJapao"))
        {
            stoneGameObj = other.gameObject;
            canInteract = true;
        }

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
        if (other.gameObject.CompareTag("PedraJapao"))
        {
            dialogueManager.EndDialogue();
            stoneGameObj = null;
            canInteract = false;
        }

        if (other.gameObject.CompareTag("MiniShrine"))
        {
            shrineObj = null;
            miniShrine = false;
        }
    }

    public void HandleStoneInteract()
    {
        if (stoneGameObj != null)
        {
            dialogueManager = stoneGameObj.GetComponent<DialogueManager>();
            dialogueManager.HandleDialogue();
        }
    }

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
