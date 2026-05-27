using System.Collections;
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
    [SerializeField] GameObject canvas;
    [SerializeField] int objChildCount = 3;

    public int shrineCounter = 0;
    public float timeToCloseUi = 3f;

    private void Start()
    {
        //dialogueManager = stoneGameObj.GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PedraJapao"))
        {
            canvas = other.transform.GetChild(objChildCount).gameObject;
            canvas.SetActive(true);
            stoneGameObj = other.gameObject;
            dialogueManager = stoneGameObj.GetComponent<DialogueManager>();
            canInteract = true;
        }

        if (other.gameObject.CompareTag("MiniShrine"))
        {
            shrineObj = other.gameObject;
            canvas = other.transform.GetChild(objChildCount).gameObject;
            canvas.SetActive(true);
            miniShrine = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            if(canvas != null) canvas.SetActive(false);
            miniShrine = false;
            shrineObj = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PedraJapao"))
        {
            canvas.SetActive(false);
            canvas = null;
            if (dialogueManager.isDialogueActive) dialogueManager.EndDialogue();
            stoneGameObj = null;
            canInteract = false;
        }

        if (other.gameObject.CompareTag("MiniShrine"))
        {
            canvas.SetActive(false);
            canvas = null;
            shrineObj = null;
            miniShrine = false;
        }

        if (other.gameObject.CompareTag("Untagged"))
        {
            if (other.transform.GetChild(objChildCount).gameObject.CompareTag("UI") && canvas != null)
            {
                canvas.SetActive(false);
                canvas = null;
            }
            else
            {
                return;
            }
        }
    }

    public void HandleStoneInteract()
    {
        if (stoneGameObj != null)
        {
            dialogueManager.HandleDialogue();
        }
    }

    public void MiniShrineInteract()
    {
        if(shrineObj != null)
        {
            shrineCounter++;
            StartCoroutine(CloseCounterUi());
            shrineCounterUi.SetActive(true);
            shrineCounterTxt.text = shrineCounter.ToString() + "/7";
            shrineObj.tag = "Untagged";
        }
    }

    IEnumerator CloseCounterUi()
    {
        yield return new WaitForSeconds(timeToCloseUi);
        shrineCounterUi.SetActive(false);
    }

    public IEnumerator OpenProgressionUi()
	{
		shrineCounterUi.SetActive(true);
		yield return new WaitForSeconds(timeToCloseUi);
		shrineCounterUi.SetActive(false);
	}
}
