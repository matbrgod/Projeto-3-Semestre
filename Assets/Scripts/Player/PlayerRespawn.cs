using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // script de respawn e checkpoint do jogador
    public Transform respawnPoint;
    GameObject newRespawn;
    GameObject oldRespawn;
    public float spawnValue;

    //private void Start()
    //{
    //    RespawnPlayer();
    //}

    private void Update()
    {
        if (transform.position.y < -spawnValue)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ChaoMata"))
        {
            RespawnPlayer();
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
