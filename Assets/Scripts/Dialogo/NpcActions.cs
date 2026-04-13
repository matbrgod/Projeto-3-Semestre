using System.Globalization;
using UnityEngine;

public class NpcActions : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public string npcName;

    void Start()
    {
        if (npcName == null)
            npcName = gameObject.name;
    }

    public void Interact()
    {
        dialogueManager.StartDialogue(npcName);
    }
}
