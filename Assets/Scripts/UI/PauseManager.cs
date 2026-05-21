using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Telas")]
    public GameObject pauseScreen;

    public bool isPaused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isPaused = false;
        pauseScreen.SetActive(false);
    }

    public void OpenScreen(int tela)
    {
        switch (tela)
        {
            case 0:
                ResumeGame();
                break;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseScreen.SetActive(isPaused);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseScreen.SetActive(isPaused);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
