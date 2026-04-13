using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

[System.Serializable]
public class DialogueEnter { public string npcName; public NodeDialogue[] node; }

[System.Serializable]
public class NodeDialogue { public string nodeId; public string npcTxt; public string choiceTrigger; public Choice[] choices; }

[System.Serializable]
public class Choice { public string choiceTxt; public string nextNodeId; }

[System.Serializable]
public class DatabaseDialogue { public DialogueEnter[] dialogues; }

public class DialogueManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject btnPrefab;
    public GameObject panelUI;
    public TMP_Text npcNameTxt;
    public TMP_Text dialogueTxt;
    public Transform btnLocal;

    public static event Action<string> OnDialogueTrigger;

    private Dictionary<string, Dictionary<string, NodeDialogue>> dialoguesBase = new Dictionary<string, Dictionary<string, NodeDialogue>>();
    private Dictionary<string, NodeDialogue> npcNode;

    private List<GameObject> btnPool = new List<GameObject>();

    int language;

    private void Start()
    {
        LoadDialogue(); // erro
    }

    void LoadDialogue()
    {
        jsonFile = Resources.Load<TextAsset>("dialogos_PT-BR");

        DatabaseDialogue dbDialogue = JsonUtility.FromJson<DatabaseDialogue>(jsonFile.text);

        foreach (var entry in dbDialogue.dialogues)
        {
            var nodeDictionary = new Dictionary<string, NodeDialogue>();

            foreach (var node in entry.node) // erro tambem
            {
                nodeDictionary[node.nodeId] = node;
            }

            dialoguesBase[entry.npcName] = nodeDictionary;
        }

        panelUI.SetActive(false);
    }

    public void StartDialogue(string npcName)
    {
        if (dialoguesBase.TryGetValue(npcName, out npcNode))
        {
            npcNameTxt.text = npcName;
            panelUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GoToNode("inicio");
        }
        else
        {
            Debug.LogWarning($"NPC {npcName} não encontrado no banco de dados.");
        }
    }

    public void EndDialogue()
    {
        BtnDeactivate();
        panelUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void BtnConfig(int index, string btnName, UnityEngine.Events.UnityAction action)
    {
        GameObject btnObject;

        if (index >= btnPool.Count)
        {
            btnObject = Instantiate(btnPrefab, btnLocal);
            btnPool.Add(btnObject);
        }
        else
        {
            btnObject = btnPool[index];
        }

        btnObject.SetActive(true);
        btnObject.GetComponentInChildren<TextMeshProUGUI>().text = btnName;

        Button btn = btnObject.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action);
    }

    void BtnDeactivate()
    {
        for (int i = 0; i < btnPool.Count; i++)
        {
            btnPool[i].gameObject.SetActive(false);
        }
    }

    public void GoToNode(string nodeId)
    {
        if (!npcNode.TryGetValue(nodeId, out NodeDialogue targetNode))
        {
            Debug.LogError($"Não achou o node '{nodeId}'");
            return;
        }

        if (!string.IsNullOrEmpty(targetNode.choiceTrigger))
        {
            OnDialogueTrigger?.Invoke(targetNode.choiceTrigger);
        }

        dialogueTxt.text = targetNode.npcTxt;
        BtnDeactivate();

        if (targetNode.choices == null || targetNode.choices.Length == 0)
        {
            BtnConfig(0, "Sair", EndDialogue);
        }
        for (int i = 0; i < targetNode.choices.Length; i++)
        {
            string choiceTxt = targetNode.choices[i].choiceTxt;
            string nextId = targetNode.choices[i].nextNodeId;

            BtnConfig(i, choiceTxt, () => GoToNode(nextId));
        }
    }
}
