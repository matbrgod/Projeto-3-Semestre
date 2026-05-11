using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Lógica funcionando para a pedra do japão

    public static DialogueManager instance;

    [Header("Refs")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private TextMeshProUGUI characterNameTxt;
    //DialogueTrigger dialogueTrigger;
    Dialogue dialogueData;
    PlayerInteract playerInteract;

    public float velFala = 3f;

    private int dialogueIndex;
    private bool isDialogueActive, isTyping, endLine;

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;

        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueData = dialogue;

        dialoguePanel.SetActive(true);

        NextLine();
    }

    public void NextLine()
    {
        if (!isDialogueActive || dialogueData == null) return;

        if(playerInteract.shrineCounter >= dialogueData.dialogueLines.Count)
        {

        }
    }
}
