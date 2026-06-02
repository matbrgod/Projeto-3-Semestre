using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CliqueE : MonoBehaviour
{
    [Tooltip("Scene name to load when E is pressed.")]
    [SerializeField]
    private string sceneToLoad;

    private bool isLoading;
    private InputAction loadAction;

    private void OnEnable()
    {
        if (loadAction == null)
        {
            loadAction = new InputAction("LoadScene", binding: "<Keyboard>/e");
            loadAction.performed += OnLoadPerformed;
        }

        loadAction.Enable();
    }

    private void OnDisable()
    {
        if (loadAction != null)
        {
            loadAction.Disable();
        }
    }

    private void OnLoadPerformed(InputAction.CallbackContext context)
    {
        if (isLoading)
            return;

        LoadScene();
    }

    private void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning("CliqueE: sceneToLoad is not set.");
            return;
        }

        isLoading = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
