using UnityEngine;
using UnityEngine.Rendering.Universal; // Importante para URP

public class movimento_nuvens : MonoBehaviour
{
    public float velocidadeX = 0.1f;
    public float velocidadeY = 0.05f;

    private UniversalAdditionalLightData _lightData;

    void Start()
    {
        // Pega o componente adicional de luz do URP
        _lightData = GetComponent<UniversalAdditionalLightData>();

        if (_lightData == null)
        {
            Debug.LogError("UniversalAdditionalLightData não encontrado! Verifique se é URP.");
        }
    }

    void Update()
    {
        if (_lightData != null)
        {
            // Acessa e altera o offset (Vector2)
            Vector2 novoOffset = _lightData.lightCookieOffset;
            novoOffset.x += velocidadeX * Time.deltaTime;
            novoOffset.y += velocidadeY * Time.deltaTime;

            _lightData.lightCookieOffset = novoOffset;
        }
    }
}
