using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[System.Serializable]
//public class CharacterInfo { public string name; }

//[System.Serializable]
//public class DialogueLines { public string name; public int lineIndex; [TextArea(2, 4)] public string line; }

//public class Dialogue { public List<DialogueLines> dialogueLines; }

//public class DialogueTrigger : MonoBehaviour
//{
//    [Header("Refs")]
//    public Dialogue dialogue;
//    public GameObject player;
//    InputManager input;

//    public bool playerInRange;

//    private void Update()
//    {
//        if (playerInRange && input.interactInput)
//        {
//            TriggerDialogue();
//        }
//    }

//    private void TriggerDialogue()
//    {
//        //DialogueManager.instance.StartDialogue(dialogue);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            player = other.gameObject;
//            playerInRange = true;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            player = null;
//            playerInRange = false;
//        }
//    }
//}
