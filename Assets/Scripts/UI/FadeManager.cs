using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    InputManager inputManager;

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
    }
}
