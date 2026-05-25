using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgMusic;
    public AudioClip stepSfx;
    public AudioClip jumpSfx;
    public AudioClip landSfx;
    public AudioClip btnSfx;

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
        musicSource = GetComponentInChildren<AudioSource>();
        sfxSource = GetComponentInChildren<AudioSource>();

        if (bgMusic != null) musicSource.clip = bgMusic;
        if (bgMusic != null) musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
