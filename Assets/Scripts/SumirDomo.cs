using UnityEngine;

public class SumirDomo : MonoBehaviour
{
    PlayerInteract playerInteract;
    public GameObject plataformas;
     void Awake()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(playerInteract.shrineCounter >= 5)
        {
            plataformas.SetActive(true);
            Destroy(gameObject);
        }
    }
}
