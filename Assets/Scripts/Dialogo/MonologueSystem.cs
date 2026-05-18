using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class MonologueLines { [TextArea(2, 4)] public string line; }

[System.Serializable]
public class Monologue { public List<MonologueLines> monologueLines; }

public class MonologueSystem : MonoBehaviour
{
    // diálogo interno do protagonista

    public static MonologueSystem instance;

    [Header("Refs")]
    [SerializeField] private GameObject monologuePanel;
    [SerializeField] private TextMeshProUGUI monologueTxt;
    public Monologue monologueData;
    PlayerInteract playerInteract;

    public float speachVel = 3f;
    public float endMonologue = 3f;

    private int monologueIndex;
    public bool isMonologueActive, isTyping = false;

    private void Start()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }

    public void HandleMonologue()
    {
        Startmonologue(monologueData);
    }

    public void Startmonologue(Monologue monologue)
    {
        monologuePanel.SetActive(true);

        isMonologueActive = true;
        monologueIndex = playerInteract.shrineCounter;
        monologueData = monologue;

        NextLine();
    }

    public void NextLine()
    {
        if (monologueIndex > monologueData.monologueLines.Count) EndMonologue();

        if (monologueData.monologueLines[monologueIndex].line == "")
        {
            Debug.LogWarning($"A fala {monologueIndex} tá vazia");
        }

        MonologueLines currentLine = monologueData.monologueLines[monologueIndex];

        StartCoroutine(TypeLine(currentLine));
    }

    IEnumerator TypeLine(MonologueLines line)
    {
        isTyping = true;
        monologueTxt.SetText("");

        foreach (char letter in line.line.ToCharArray())
        {
            monologueTxt.text += letter;
            yield return new WaitForSeconds(speachVel);
        }
        isTyping = false;

        yield return new WaitForSeconds(endMonologue);
        EndMonologue();
    }

    public void EndMonologue()
    {
        StopAllCoroutines();
        monologuePanel.SetActive(false);
        isMonologueActive = false;
        isTyping = false;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleMonologue();
        }
    }
}
