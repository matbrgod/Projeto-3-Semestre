using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header("Botões")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private TextMeshProUGUI txtPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private TextMeshProUGUI txtOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private TextMeshProUGUI txtCredits;
    [SerializeField] private Button btnSair;
    [SerializeField] private TextMeshProUGUI txtSair;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OpenMenu(int menu)
    {
        switch (menu)
        {
            case 0:
                BtnMenu();
                break;
            case 1:
                BtnOptions();
                break;
            case 2:
                BtnCredits();
                break;
            case 3:
                BtnControls();
                break;
            case 4:
                QuitGame();
                break;
        }
    }

    public void BtnMenu()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BtnOptions()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BtnCredits()
    {
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BtnControls()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //public void BtnDeactivate(Button bot, TextMeshProUGUI text)
    //{
    //    if (bot == null) return;
    //    bot.interactable = false;
    //    text.color = Color.black;
    //}

    //public void BtnActivate(Button bot, TextMeshProUGUI text)
    //{
    //    if (bot == null) return;
    //    bot.interactable = true;
    //    text.color = Color.white;
    //}
}
