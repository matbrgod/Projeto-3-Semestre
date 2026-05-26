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
    public float waitToSpeak = 1f;

    [SerializeField] private int monologueIndex = 0;
    public bool isMonologueActive, isTyping = false;

    private void Update()
    {
        if (monologueIndex > monologueData.monologueLines.Count)
        {
            EndMonologue();
        }
    }

    public void HandleMonologue()
    {
        Startmonologue(monologueData);
    }

    public void Startmonologue(Monologue monologue)
    {
        monologuePanel.SetActive(true);

        monologueIndex = 0;
        isMonologueActive = true;
        monologueData = monologue;

        NextLine();
    }

    public void NextLine()
    {
        //if (monologueData.monologueLines[monologueIndex].line == "")
        //{
        //    Debug.LogWarning($"A fala {monologueIndex} tá vazia");
        //}

        MonologueLines currentLine = monologueData.monologueLines[monologueIndex];
        StartCoroutine(TypeLine(currentLine));
    }

    IEnumerator TypeLine(MonologueLines line)
    {
        int index = monologueIndex + 1;

        isTyping = true;
        monologueTxt.SetText("");

        foreach (char letter in line.line.ToCharArray())
        {
            monologueTxt.text += letter;
            yield return new WaitForSeconds(speachVel);
        }
        
        yield return new WaitForSeconds(waitToSpeak);

        if (index >= monologueData.monologueLines.Count)
        {
            EndMonologue();
        }
        else //(monologueIndex++ <= monologueData.monologueLines.Count)
        {
            monologueIndex++;
            NextLine();
        }
    }

    public void EndMonologue()
    {
        monologuePanel.SetActive(false);
        this.gameObject.SetActive(false);
        monologueIndex = 0;
        isMonologueActive = false;
        isTyping = false;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleMonologue();
        }
    }
}
