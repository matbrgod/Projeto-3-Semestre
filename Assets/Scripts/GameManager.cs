using UnityEngine;

public class GameManager : MonoBehaviour
{
      void Awake()
    {
        // Define o limite para 60 FPS
        Application.targetFrameRate = 60;
        
        // Desativa o VSync para o targetFrameRate funcionar
        QualitySettings.vSyncCount = 0; 
    }
}
