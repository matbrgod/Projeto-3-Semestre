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

    public float speachVel = 3f;
    public float endMonologue = 3f;

    private int monologueIndex = 0;
    public bool isMonologueActive, isTyping = false;

    public void HandleMonologue()
    {
        Startmonologue(monologueData);
    }

    public void Startmonologue(Monologue monologue)
    {
        monologuePanel.SetActive(true);

        isMonologueActive = true;
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
        monologueIndex++;
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

        if (monologueIndex++ > monologueData.monologueLines.Count)
        {
            yield return new WaitForSeconds(endMonologue);
            EndMonologue();
        }
        else
        {
            monologueIndex++;
            NextLine();
        }
    }

    public void EndMonologue()
    {
        StopAllCoroutines();
        monologueIndex = 0;
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
