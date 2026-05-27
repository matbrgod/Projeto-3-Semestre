using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip menuBg;
    public AudioClip gameBg;
    public AudioClip stepSfx;
    public AudioClip jumpSfx;
    public AudioClip landSfx;
    public AudioClip btnSfx;
    AudioClip bgMusic;

    string cena;

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

        musicSource = GetComponentInChildren<AudioSource>();
        sfxSource = GetComponentInChildren<AudioSource>();
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
        else if (cena.Contains("yuri-testes") || cena.Contains("blocagem_matheus"))
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
}
