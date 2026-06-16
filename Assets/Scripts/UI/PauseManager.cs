using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    AudioManager audioManager;
    InputManager inputManager;
    public GameObject cameraManager;

    float camMoveSpeed;

    [Header("Telas")]
    public GameObject pauseScreen;
    public GameObject optionsScreen;
    public GameObject controlsScreen;

    public bool isPaused;

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1f;

        DontDestroyOnLoad(gameObject);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        inputManager = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        cameraManager = GameObject.Find("CameraManager");

        camMoveSpeed = cameraManager.GetComponent<CameraManager>().camLookSpeed;

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
                BtnOptions();
                break;
            case 2:
                BtnQuitToMenu();
                break;
            case 3:
                BtnControls();
                break;
            case 4:
                BtnReturn();
                break;
            case 5:
                BtnReturnToOptions();
                break;
        }
    }

    private void BtnResume()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        ResumeGame();
    }

    private void BtnOptions()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    private void BtnQuitToMenu()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(false);
        SceneManager.LoadScene("menu");
    }

    private void BtnReturn()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    private void BtnControls()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    private void BtnReturnToOptions()
    {
        if (audioManager != null) audioManager.PlaySfx(audioManager.btnSfx);
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        inputManager.playerControl.PlayerMove.Enable();
        cameraManager.GetComponent<CameraManager>().camLookSpeed = camMoveSpeed;
        cameraManager.GetComponent<CameraManager>().camPivotSpeed = camMoveSpeed;
        isPaused = false;
        pauseScreen.SetActive(isPaused);
        optionsScreen.SetActive(isPaused);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        isPaused = true;
        inputManager.playerControl.PlayerMove.Disable();
        Time.timeScale = 0f;
        cameraManager.GetComponent<CameraManager>().camLookSpeed = 0f;
        cameraManager.GetComponent<CameraManager>().camPivotSpeed = 0f;
        pauseScreen.SetActive(isPaused);
        optionsScreen.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
