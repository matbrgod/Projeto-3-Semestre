using UnityEngine;

public class DestruirBarreira : MonoBehaviour
{
    PlayerInteract playerInteract;
     void Awake()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(playerInteract.shrineCounter >= 2)
        {
            Destroy(gameObject);
        }
    }
}
