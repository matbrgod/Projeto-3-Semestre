using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.MaterialUpgrader;

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
    //DialogueTrigger dialogueTrigger;
    public Dialogue dialogueData;
    PlayerInteract playerInteract;
    InputManager input;

    public float speachVel = 3f;

    private int dialogueIndex;
    private bool isDialogueActive, isTyping = false;

    private void Start()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }

    public void HandleDialogue()
    {
        Debug.Log("Chamou HandleDialogue");
        //if (dialogueData == null || (input.pauseInput && isDialogueActive)) return;

        //if (isDialogueActive) NextLine();
        //else StartDialogue(dialogueData);

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
        if (dialogueIndex > dialogueData.dialogueLines.Count) return;

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
}
