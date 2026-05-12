using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLines { public string name; [TextArea(2, 4)] public string line; }

[System.Serializable]
public class Dialogue { public List<DialogueLines> dialogueLines; }

public class DialogueManager : MonoBehaviour
{
    // Lógica funcionando para a pedra do japão

    public static DialogueManager instance;

    [Header("Refs")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private TextMeshProUGUI characterNameTxt;
    public Dialogue dialogueData;
    PlayerInteract playerInteract;
    InputManager input;

    public float speachVel = 3f;

    private int dialogueIndex;
    public bool isDialogueActive, isTyping = false;

    private void Start()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
        input = FindFirstObjectByType<InputManager>();
    }

    private void Update()
    {
        if (isDialogueActive && input.interactInput)
        {
            NextLine();
        }
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
        characterNameTxt.text = dialogueData.dialogueLines[dialogueIndex].name;

        NextLine();
    }

    public void NextLine()
    {
        if (dialogueIndex > dialogueData.dialogueLines.Count) EndDialogue();

        if (dialogueData.dialogueLines[dialogueIndex].line == "")
        {
            Debug.LogWarning($"A fala {dialogueIndex} tá vazia");
        }

        if (dialogueData.dialogueLines[dialogueIndex].name == "") characterNameTxt.text = gameObject.name;
        else characterNameTxt.text = dialogueData.dialogueLines[dialogueIndex].name;

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

        yield return new WaitForSeconds(3.0f);
        EndDialogue();
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isTyping = false;

        //if (characterNameTxt.text != null) characterNameTxt.SetText("");
        //if (dialogueTxt.text != null) dialogueTxt.SetText("");
    }
}
