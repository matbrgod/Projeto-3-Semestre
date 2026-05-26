using UnityEngine;

public class PauseManager : MonoBehaviour
{
    AudioManager audioManager;
    InputManager inputManager;

    [Header("Telas")]
    public GameObject pauseScreen;
    [SerializeField] GameObject quitScreen;

    public bool isPaused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        inputManager = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        isPaused = false;
        pauseScreen.SetActive(false);
    }

    public void OpenScreen(int tela)
    {
        switch (tela)
        {
            case 0:
                BtnResume();
                break;
            case 1:
                BtnSaveAndQuit();
                break;
            case 2:
                BtnQuitToMenu();
                break;
        }
    }

    public void BtnResume()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        ResumeGame();
    }

    void BtnSaveAndQuit()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
    }

    void BtnQuitToMenu()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(false);
        quitScreen.SetActive(true);
    }

    void BtnDontSave()
    {

    }

    void Save()
    {

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
        inputManager.playerControl.Disable();
        pauseScreen.SetActive(isPaused);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
