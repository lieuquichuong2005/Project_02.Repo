using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class HomeManager : MonoBehaviour
{
    [Header("Button")]
    public Button startGameButton;
    public Button accountManagerButton;
    public Button settingButton;
    public Button helpButton;
    public Button creditButton;
    public Button quitButton;
    public Button closeButton;

    [Header("Panel")]
    public List<GameObject> panels;
    public GameObject logInPanel;
    public GameObject registerPanel;

    [Header("LogIn")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button switchToLogIn;
    public Button LogIn;

    [Header("Register")]
    public TMP_InputField usernameInputRegister;
    public TMP_InputField passwordInputRegister;
    public TMP_InputField confirmPasswordInputRegister;
    public Button switchToRegister;
    public Button Register;

    public GameObject listButton;
    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartButton);
        accountManagerButton.onClick.AddListener(OnAccountManagementButton);
        settingButton.onClick.AddListener(OnSettingsButton);
        helpButton.onClick.AddListener(OnHelpButton);
        creditButton.onClick.AddListener(OnCreditsButton);
        quitButton.onClick.AddListener(OnQuitButton);
        closeButton.onClick.AddListener(OnCloseButton);

    }

    void Start()
    {
        SetActivePanel(null);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void OnAccountManagementButton()
    {
        SetActivePanel(panels[0]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnSettingsButton()
    {
        SetActivePanel(panels[1]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnHelpButton()
    {
        SetActivePanel(panels[2]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnCreditsButton()
    {
        SetActivePanel(panels[3]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnQuitButton()
    {
        Application.Quit(); 
    }

    public void OnSwitchToRegister()
    {
        logInPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnLoginWithFacebook()
    {
        registerPanel.SetActive(false);
        logInPanel.SetActive(true);
    }

    public void OnLoginWithEmail()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {

        }
        else
        {

        }
    }
    public void SetActivePanel(GameObject panelToActivate)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(panel == panelToActivate); 

        }
        if (panelToActivate != null)
        {
            listButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-430, -100);
        }
        else
        {
            listButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
            closeButton.gameObject.SetActive(false);
        }
    }
    void OnCloseButton()
    {
        SetActivePanel(null);
    }    

}