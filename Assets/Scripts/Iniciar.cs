using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Iniciar : MonoBehaviour
{
    [SerializeField]
    GameObject LoadScreen_screen;
    [SerializeField]
    Image LoadFill;

    public void LoadarScene(int cena)
    {
        StartCoroutine(LoadSceneAsync(cena));
        
    }
    private IEnumerator LoadSceneAsync(int cena)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(cena);
        LoadScreen_screen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.4f);
            LoadFill.fillAmount = progress;
            yield return null;
        }
        LoadScreen_screen.SetActive(false);
    }
}
