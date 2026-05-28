using UnityEngine;

public class Dialogo5 : MonoBehaviour
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
        if (playerInteract.shrineCounter == 5)
        {
            triggerDialogo.SetActive(true);
        }
    }
}
