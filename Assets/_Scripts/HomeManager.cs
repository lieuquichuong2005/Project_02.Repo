using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Photon.Pun.Demo.PunBasics;

public class HomeManager : MonoBehaviour
{
    public FirebareManager firebaseManager;
    [Header("Button")]
    public Button startGameButton;
    public Button accountManagerButton;
    public Button settingButton;
    public Button helpButton;
    public Button creditButton;
    public Button quitButton;
    public Button closeButton;
    public Button logOutButton;

    public GameObject listButton;

    public TMP_Text notifyText;
    public TMP_Text accountNameText;
    public string displayName;

    [Header("Panel")]
    public GameObject loadingPanel;
    public GameObject menuPanel;
    public List<GameObject> panels;
    public GameObject logInPanel;
    public GameObject registerPanel;
    public GameObject notifyPanel;
    public GameObject nameTextPanel;

    [Header("LogIn")]
    public TMP_InputField emailInputLogIn;
    public TMP_InputField passwordInputLogIn;
    public Button switchToLogInButton;
    public Button logInButton;

    [Header("Register")]
    public TMP_InputField emailInputRegister;
    public TMP_InputField passwordInputRegister;
    public TMP_InputField confirmPasswordInputRegister;
    public Button switchToRegisterButton;
    public Button registerButton;

    [Header("SettingPanel")]
    public TMP_Dropdown graphicsDropdown;
    public Toggle fullscreenToggle;
    //public Dropdown languageDropdown;

    private void Awake()
    {
        PlayerPrefs.SetInt("isTester", 0);
        // Khởi tạo Firebase
        
        startGameButton.onClick.AddListener(OnStartButton);
        accountManagerButton.onClick.AddListener(OnAccountManagementButton);
        settingButton.onClick.AddListener(OnSettingsButton);
        helpButton.onClick.AddListener(OnHelpButton);
        creditButton.onClick.AddListener(OnCreditsButton);
        quitButton.onClick.AddListener(OnQuitButton);
        closeButton.onClick.AddListener(OnCloseButton);
        logOutButton.onClick.AddListener(firebaseManager.LogOutAccount);

        //chooseCharacterButton.onClick.AddListener(OnChooseCharacterScene);


        switchToLogInButton.onClick.AddListener(OnSwitchToLogin);
        switchToRegisterButton.onClick.AddListener(OnSwitchToRegister);
        logInButton.onClick.AddListener(firebaseManager.LoginAccout);
        registerButton.onClick.AddListener(firebaseManager.RegisterAccout);

        menuPanel.gameObject.SetActive(false);
        loadingPanel.gameObject.SetActive(true);

        Debug.Log(displayName);

        StartCoroutine(HideLoadingEffect());
    }

    void Start()
    {
        if (firebaseManager.auth.CurrentUser != null)
        {
            displayName = firebaseManager.auth.CurrentUser.Email.Split('@')[0];
            if (displayName == "test")
            {
                PlayerPrefs.SetInt("isTester", 1);
            }
            DisplayPlayerName(displayName);
            logOutButton.gameObject.SetActive(true);
            nameTextPanel.SetActive(true);
        }
        else
        {
            logOutButton.gameObject.SetActive(false);
            nameTextPanel.SetActive(false);
        }

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

        if (firebaseManager.auth.CurrentUser != null)
        {
            accountNameText.text = $"Logged in as: {displayName}"; 
            nameTextPanel.gameObject.SetActive(true);
            logOutButton.gameObject.SetActive(true); 

            DisplayPlayerName(displayName); 
            SceneManager.LoadScene("ChooseCharacterScene");

        }
        else
        {
            notifyPanel.SetActive(true);
            notifyText.text = "Please log in to start the game!";
            StartCoroutine(HideNotify());
        }

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

    public void OnLogInWithFacebook()
    {
        AudioManager.instance.PlayClickSound();
    }

    public void SetActivePanel(GameObject panelToActivate)
{
    foreach (GameObject panel in panels)
    {
        panel.SetActive(panel == panelToActivate);

        // Lấy nút tương ứng với panel
        Button button = GetButtonForPanel(panel);
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>(); // Hoặc Text nếu bạn dùng UI Text

        if (buttonText != null)
        {
            buttonText.color = panel == panelToActivate ? Color.green : Color.red; // Xanh lá cây nếu đang hoạt động, đỏ nếu không
        }
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

    private Button GetButtonForPanel(GameObject panel)
{
    if (panel == panels[0]) return accountManagerButton;
    if (panel == panels[1]) return settingButton;
    if (panel == panels[2]) return helpButton;
    if (panel == panels[3]) return creditButton;

    return null; 
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

    public void DisplayPlayerName(string name)
    {
        nameTextPanel.SetActive(true);

        accountNameText.gameObject.SetActive(true);

        accountNameText.text = $"Welcome, {name}!"; // Hiển thị tên người chơi
    }
    IEnumerator HideNotify()
    {
        yield return new WaitForSeconds(2);
        notifyPanel.SetActive(false);
        notifyText.text = " ";
    }
    IEnumerator HideLoadingEffect()
    {
        yield return new WaitForSeconds(2);
        loadingPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);
    }
}
