using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !PlayerRespawner.instance.isRespawning)
        {
            PlayerRespawner.instance.isRespawning = true;
            StartCoroutine(FadeManager.instance.HandleFadeIn());
        }
    }
}
