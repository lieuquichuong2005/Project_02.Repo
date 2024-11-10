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

    public GameObject listButton;

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

    [Header("SettingPanel")]
    public TMP_Dropdown graphicsDropdown;
    public Toggle fullscreenToggle;
    //public Dropdown languageDropdown;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartButton);
        accountManagerButton.onClick.AddListener(OnAccountManagementButton);
        settingButton.onClick.AddListener(OnSettingsButton);
        helpButton.onClick.AddListener(OnHelpButton);
        creditButton.onClick.AddListener(OnCreditsButton);
        quitButton.onClick.AddListener(OnQuitButton);
        closeButton.onClick.AddListener(OnCloseButton);
        switchToLogIn.onClick.AddListener(OnSwitchToLogin);
        switchToRegister.onClick.AddListener(OnSwitchToRegister);

    }

    void Start()
    {
        graphicsDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 1); 
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        //languageDropdown.value = PlayerPrefs.GetInt("Language", 0); 

        graphicsDropdown.onValueChanged.AddListener(OnGraphicsQualityChange);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);
        //languageDropdown.onValueChanged.AddListener(OnLanguageChange);

        SetActivePanel(null);

    }

    public void OnStartButton()
    {
        AudioManager.instance.PlayClickSound();
        SceneManager.LoadScene(1);
        
    }

    public void OnAccountManagementButton()
    {
        AudioManager.instance.PlayClickSound();
        SetActivePanel(panels[0]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnSettingsButton()
    {
        AudioManager.instance.PlayClickSound();
        SetActivePanel(panels[1]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnHelpButton()
    {
        AudioManager.instance.PlayClickSound();
        SetActivePanel(panels[2]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnCreditsButton()
    {
        AudioManager.instance.PlayClickSound();
        SetActivePanel(panels[3]);
        closeButton.gameObject.SetActive(true);
    }

    public void OnQuitButton()
    {
        AudioManager.instance.PlayClickSound();
        Application.Quit(); 
    }

    public void OnSwitchToRegister()
    {
        AudioManager.instance.PlayClickSound();
        logInPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnSwitchToLogin()
    {
        AudioManager.instance.PlayClickSound();
        registerPanel.SetActive(false);
        logInPanel.SetActive(true);
    }

    public void OnLoginWithEmail()
    {
        AudioManager.instance.PlayClickSound();
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {

        }
        else
        {

        }
    }
    public void OnLogInWithFacebook()
    {
        AudioManager.instance.PlayClickSound();
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
        AudioManager.instance.PlayClickSound();
        SetActivePanel(null);
    }
    public void OnGraphicsQualityChange(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        PlayerPrefs.Save();
    }
    public void OnFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnLanguageChange(int languageIndex)
    {
        PlayerPrefs.SetInt("Language", languageIndex);
        PlayerPrefs.Save();
        //UpdateLanguage(languageIndex);
    }

    /*private void UpdateLanguage(int languageIndex)
    {
        // Logic để cập nhật ngôn ngữ trong game
        // Ví dụ: sử dụng một bảng từ điển để lấy văn bản
    }*/
}