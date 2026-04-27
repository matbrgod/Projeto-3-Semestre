using TMPro;
using UnityEngine;

public class InteractMiniShrine : MonoBehaviour
{
    [Header("Mini Shrine Id")]
    public bool miniShrine = false;
    public GameObject shrineObj;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI shrineCounterTxt;
    [SerializeField] GameObject shrineCounterUi;

    int shrineCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MiniShrine"))
        {
            miniShrine = true;
            shrineObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MiniShrine"))
        {
            miniShrine = false;
            shrineObj = null;
        }
    }

    public void MiniShrineInteract()
    {
        Debug.Log("Chamou MiniShrineInteract");
        shrineCounterUi.SetActive(true);
        shrineCounter++;
        shrineObj.tag = "Untagged";
    }
}
