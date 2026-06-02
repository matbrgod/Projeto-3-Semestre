using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // script de respawn e checkpoint do jogador
    public float spawnValue;
    public bool isRespawning;
    public Transform respawnPoint;
    GameObject newRespawn;
    GameObject oldRespawn;
    PlayerManager playerManager;
    InputManager input;
    PlayerMovement move;
    Animator animator;
    //AnimatorManager animator;

    private void Start()
    {
        //RespawnPlayer();
        playerManager = GetComponent<PlayerManager>();
        input = GetComponent<InputManager>();
        move = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        //animator = GetComponent<AnimatorManager>();
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
        isRespawning = true;
        input.moveAmout = 0f;
        input.verticalInput = 0f;
        input.playerControl.PlayerMove.Disable();
        input.enabled = false;
        move.enabled = false;
        move.isWalking = false;
        animator.enabled = false;
        transform.position = respawnPoint.position;
        playerManager.enabled = true;
        move.enabled = true;
        StartCoroutine(FadeManager.instance.HandleFadeOut());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ChaoMata"))
        {
            StartCoroutine(FadeManager.instance.HandleFadeIn());
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
