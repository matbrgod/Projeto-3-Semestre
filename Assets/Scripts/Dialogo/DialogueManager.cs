using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLines { public string line; }

[System.Serializable]
public class Dialogue { public List<DialogueLines> dialogueLines; }

public class DialogueManager : MonoBehaviour
{
    // Lógica funcionando para a pedra do japão / com o contador de shrines/conhecimento

    public static DialogueManager instance;

    [Header("Refs")]
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    public Dialogue dialogueData;
    PlayerInteract playerInteract;

    public int dialogueIndex;
    public bool finishedDialogue, isDialogueActive, isTyping = false;


    private void Start()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }

    public void HandleDialogue()
    {
        StartDialogue(dialogueData);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIndex = playerInteract.shrineCounter;
        if (dialogueIndex > dialogueData.dialogueLines.Count) return;

        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        finishedDialogue = false;

        NextLine();
    }

    public void NextLine()
    {
        if (dialogueIndex > dialogueData.dialogueLines.Count) EndDialogue();

        if (dialogueData.dialogueLines[dialogueIndex].line == "")
        {
            Debug.LogWarning($"A fala {dialogueIndex} tá vazia");
        }

        DialogueLines currentLine = dialogueData.dialogueLines[dialogueIndex];
        dialogueTxt.SetText(currentLine.line);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isTyping = false;
        finishedDialogue = true;
    }
}
