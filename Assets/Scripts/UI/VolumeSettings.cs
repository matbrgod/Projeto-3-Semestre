using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMix;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    private void LoadVolume()
    {
        if (masterSlider != null) masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        audioMix.SetFloat("master", PlayerPrefs.GetFloat("masterVolume"));
        if (musicSlider != null) musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        audioMix.SetFloat("music", PlayerPrefs.GetFloat("musicVolume"));
        if (sfxSlider != null) sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        audioMix.SetFloat("master", PlayerPrefs.GetFloat("sfxVolume"));

        SetMasterVolume();
        SetMusicVolume();
        SetSfxVolume();
    }

    public void SetMasterVolume()
    {
        if (masterSlider == null) return;
        float volume = masterSlider.value;
        audioMix.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        if (musicSlider == null) return;
        float volume = musicSlider.value;
        audioMix.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume()
    {
        if (sfxSlider == null) return;
        float volume = sfxSlider.value;
        audioMix.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
