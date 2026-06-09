using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public static PlayerRespawner instance;
    GetCheckpoint getCheckpoint;
    public GameObject playerPrefab;
    public bool isRespawning;
    public float spawnValue;

    private void Awake()
    {
        instance = this;
        getCheckpoint = GameObject.FindWithTag("Player").GetComponent<GetCheckpoint>();
    }
    private void Update()
    {
        if (playerPrefab.transform.position.y < -spawnValue)
        {
            StartCoroutine(FadeManager.instance.HandleFadeIn());
        }
    }

    public void RespawnPlayer()
    {
        //isRespawning = true;
        playerPrefab.SetActive(false);
        playerPrefab.transform.position = getCheckpoint.spawn.position;
        playerPrefab.SetActive(true);
        //isRespawning = false;
    }
}
