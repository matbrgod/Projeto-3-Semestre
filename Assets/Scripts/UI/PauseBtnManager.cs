using UnityEngine;

public class PauseBtnManager : MonoBehaviour
{
    [Header("Telas")]
    public GameObject pauseScreen;

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
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
}
