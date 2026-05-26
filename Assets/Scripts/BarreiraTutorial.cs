using UnityEngine;

public class BarreiraTutorial : MonoBehaviour
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
        if (playerInteract.shrineCounter >= 1)
        {
            Destroy(gameObject);
            triggerDialogo.SetActive(true);
        }
    }
}
