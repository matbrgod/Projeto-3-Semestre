using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // script de respawn e checkpoint do jogador
    public Transform respawnPoint;
    GameObject newRespawn;
    GameObject oldRespawn;
    public float spawnValue;
    PlayerManager playerManager;
    InputManager input;

    private void Start()
    {
        //RespawnPlayer();
        playerManager = GetComponent<PlayerManager>();
        input = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (transform.position.y < -spawnValue)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        playerManager.enabled = false;
        transform.position = respawnPoint.position;
        playerManager.enabled = true;
        StartCoroutine(input.HandleFadeOut());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ChaoMata"))
        {
            StartCoroutine(input.HandleFadeIn());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnPoint = collision.gameObject.transform;
            oldRespawn = newRespawn;
            if (oldRespawn != null) oldRespawn.SetActive(true);
            newRespawn = collision.transform.GetChild(0).gameObject;
            newRespawn.SetActive(false);
        }
    }
}
