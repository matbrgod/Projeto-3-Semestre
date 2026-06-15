using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    
    AudioManager audioManager;
    InputManager inputManager;
    PlayerManager playerManager;
    PlayerMovement playerMovement;
    PlayerRespawner respawn;
    GameObject player;

    public Image fadeImage;
    public bool isInTransition;
    public bool isShowing;
    public float transition;
    private float duration;

    private void Awake()
    {
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        instance = this;

        player = GameObject.FindWithTag("Player");

        if(player != null)
        {           
            inputManager = player.GetComponent<InputManager>();
            playerManager = player.GetComponent<PlayerManager>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        //playerRespawn = GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>();
        respawn = FindFirstObjectByType<PlayerRespawner>();
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (!isInTransition) return;

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0)
            isInTransition = false;
    }

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
        inputManager.enabled = !isShowing;
        playerManager.enabled = !isShowing;
        playerMovement.enabled = !isShowing;
    }

    public IEnumerator HandleFadeIn()
    {
        respawn.isRespawning = true;
        audioManager.sfxSource.mute = true;
        isInTransition = true;
        Fade(true, 0.15f);
        inputManager.moveInput = Vector3.zero;
        yield return new WaitForSeconds(1f);
        respawn.RespawnPlayer();
        StartCoroutine(HandleFadeOut());
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator HandleFadeOut()
    {
        yield return new WaitForSeconds(1f);
        MonoBehaviour[] allScripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if(script != null)
            {
                script.enabled = true;
            }
        }
        Fade(false, 1f);
        respawn.isRespawning = false;
        audioManager.sfxSource.mute = false;
        yield return new WaitForSeconds(1.5f);
        isInTransition = false;
    }
}
