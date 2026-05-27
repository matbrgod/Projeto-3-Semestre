using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EfeitoConhecimento : MonoBehaviour
{
    PlayerInteract playerInteract;
    public GameObject mensagem;
    public Volume volume;
    private int lastShrineCounter;
    private Coroutine effectCoroutine;

    void Awake()
    {
        playerInteract = FindFirstObjectByType<PlayerInteract>();
        lastShrineCounter = playerInteract ? playerInteract.shrineCounter : 0;
    }

    void Update()
    {
        if (playerInteract == null)
            return;

        if (playerInteract.shrineCounter > lastShrineCounter)
        {
            lastShrineCounter = playerInteract.shrineCounter;
            if (effectCoroutine != null)
                StopCoroutine(effectCoroutine);

            effectCoroutine = StartCoroutine(PlayVolumeEffect());
        }
    }

    private IEnumerator PlayVolumeEffect()
    {
        mensagem.SetActive(true);

        if (volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            colorAdjustments.hueShift.value = 86f;

        if (volume.profile.TryGet(out LensDistortion lensDistortion))
            lensDistortion.intensity.value = -0.5f;

        yield return new WaitForSeconds(0.8f);

        if (volume.profile.TryGet(out ColorAdjustments colorAdjustmentsReset))
            colorAdjustmentsReset.hueShift.value = 0f;

        if (volume.profile.TryGet(out LensDistortion lensDistortionReset))
            lensDistortionReset.intensity.value = 0f;

        mensagem.SetActive(false);
        effectCoroutine = null;
    }
}
