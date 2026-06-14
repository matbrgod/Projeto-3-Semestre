using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    PlayerMovement playerMove;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip menuBg;
    public AudioClip gameBg;
    public AudioClip stepSfx;
    public AudioClip jumpSfx;
    public AudioClip landSfx;
    public AudioClip btnSfx;
    AudioClip bgMusic;

    string cena;
    public bool waitForStep;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource != null)
            musicSource.loop = true;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateMusic();

        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        sfxSource = transform.GetChild(1).GetComponent<AudioSource>();

        if (playerMove == null)
            playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic();
    }

    public void UpdateMusic()
    {
        cena = SceneManager.GetActiveScene().name.ToLower();

        if (cena.Contains("menu"))
            bgMusic = menuBg;
        else if (cena.Contains("yuri-testes") || cena.Contains("blocagem_matheus") || cena.Contains("Cutscene Inicial"))
            bgMusic = gameBg;

        PlayMusic(bgMusic);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if(musicSource == null) return;

        if(musicSource.clip != clip)
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public IEnumerator HandleSteps()
    {
        if (playerMove != null)
        {
            while (playerMove.isWalking)
            {
                sfxSource.PlayOneShot(stepSfx);
                waitForStep = true;
                yield return new WaitForSeconds(playerMove.moveSpeed);
                waitForStep = false;
            }
        }
    }
}
