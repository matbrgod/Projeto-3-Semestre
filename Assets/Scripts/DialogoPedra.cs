using UnityEngine;

public class DialogoPedra : MonoBehaviour
{
    DialogueManager dialogueManager;
    public GameObject monologueTrigger;
    public int linhaDeDialogo;

     void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.finishedDialogue && dialogueManager.dialogueIndex == linhaDeDialogo)
        {
            monologueTrigger.SetActive(true);
        }
    }
}
