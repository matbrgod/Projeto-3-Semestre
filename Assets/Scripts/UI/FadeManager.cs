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
    PlayerRespawn playerRespawn;

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
        playerRespawn = GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        //if (inputManager.respawnInput)
        //{
        //    Fade(true, 1f);
        //}
        //else
        //{
        //    Fade(false, 3f);
        //}

        if (!isInTransition) return;

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);

        if (transition > 1 || transition < 0)
            isInTransition = false;
    }

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
        inputManager.enabled = !showing;
        playerManager.enabled = !showing;
        playerMovement.enabled = !showing;
    }

    public IEnumerator HandleFadeIn()
    {
        isInTransition = true;
        Fade(true, 0.25f);
        yield return new WaitForSeconds(1f);
        playerRespawn.RespawnPlayer();
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator HandleFadeOut()
    {
        yield return new WaitForSeconds(1f);
        animManager.animator.enabled = true;
        animManager.animator.SetBool("isIdle", true);
        inputManager.enabled = true;
        playerRespawn.isRespawning = false;
        Fade(false, 1f);
        yield return new WaitForSeconds(1.5f);
        isInTransition = false;
    }
}
