using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    
    AnimatorManager animManager;
    InputManager inputManager;
    PlayerManager playerManager;
    PlayerMovement playerMovement;
    //PlayerRespawn playerRespawn;
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

        inputManager = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        animManager = GameObject.FindWithTag("Player").GetComponent<AnimatorManager>();
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        player = GameObject.FindWithTag("Player");
        //playerRespawn = GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>();
        respawn = FindFirstObjectByType<PlayerRespawner>();
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
        inputManager.enabled = !PlayerRespawner.instance.isRespawning;
        playerManager.enabled = !PlayerRespawner.instance.isRespawning;
        playerMovement.enabled = !PlayerRespawner.instance.isRespawning;
    }

    public IEnumerator HandleFadeIn()
    {
        isInTransition = true;
        Fade(true, 0.25f);
        yield return new WaitForSeconds(1f);
        //playerRespawn.RespawnPlayer();
        respawn.RespawnPlayer();
        yield return new WaitForSeconds(1f);
        StartCoroutine(HandleFadeOut());
    }

    public IEnumerator HandleFadeOut()
    {
        yield return new WaitForSeconds(1f);
        //animManager.animator.enabled = true;
        //animManager.animator.SetBool("isIdle", true);
        //inputManager.enabled = true;
        MonoBehaviour[] allScripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if(script != null)
            {
                script.enabled = true;
            }
        }
        respawn.isRespawning = false;
        Fade(false, 1f);
        yield return new WaitForSeconds(1.5f);
        isInTransition = false;
    }
}
