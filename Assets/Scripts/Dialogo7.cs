using UnityEngine;

public class Dialogo7 : MonoBehaviour
{
    PlayerInteract playerInteract;
    [SerializeField] GameObject triggerDialogo;
    void Awake()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteract.shrineCounter == 7)
        {
            triggerDialogo.SetActive(true);
        }
    }
}
