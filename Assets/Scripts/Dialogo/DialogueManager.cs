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
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    public Dialogue dialogueData;
    PlayerInteract playerInteract;

    public float speachVel = 3f;

    private int dialogueIndex;
    public bool isDialogueActive, isTyping = false;

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
        dialoguePanel.SetActive(true);

        isDialogueActive = true;
        dialogueIndex = playerInteract.shrineCounter;
        dialogueData = dialogue;

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

        StartCoroutine(TypeLine(currentLine));
    }

    IEnumerator TypeLine(DialogueLines line)
    {
        isTyping = true;
        dialogueTxt.SetText("");

        foreach (char letter in line.line.ToCharArray())
        {
            dialogueTxt.text += letter;
            yield return new WaitForSeconds(speachVel);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isTyping = false;
    }
}
