using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;

public class HomeManager : MonoBehaviour
{
    FirebaseAuth auth;

    [Header("Button")]
    public Button startGameButton;
    public Button accountManagerButton;
    public Button settingButton;
    public Button helpButton;
    public Button creditButton;
    public Button quitButton;
    public Button closeButton;

    public GameObject listButton;

    public TMP_Text notifyText;

    [Header("Panel")]
    public List<GameObject> panels;
    public GameObject logInPanel;
    public GameObject registerPanel;
    public GameObject playerNamePanel;

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
        // Khởi tạo Firebase
        auth = FirebaseAuth.DefaultInstance;
        // Kiểm tra xem auth đã được khởi tạo chưa
        if (auth == null)
        {
            Debug.LogError("FirebaseAuth has not been initialized.");
        }

        startGameButton.onClick.AddListener(OnStartButton);
        accountManagerButton.onClick.AddListener(OnAccountManagementButton);
        settingButton.onClick.AddListener(OnSettingsButton);
        helpButton.onClick.AddListener(OnHelpButton);
        creditButton.onClick.AddListener(OnCreditsButton);
        quitButton.onClick.AddListener(OnQuitButton);
        closeButton.onClick.AddListener(OnCloseButton);

        switchToLogInButton.onClick.AddListener(OnSwitchToLogin);
        switchToRegisterButton.onClick.AddListener(OnSwitchToRegister);
        logInButton.onClick.AddListener(LoginAccout);
        registerButton.onClick.AddListener(RegisterAccout);


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
        if (auth.CurrentUser != null)
        {
            // Nếu đã đăng nhập, bắt đầu trò chơi
            Debug.Log("Game started!");
            // Gọi hàm bắt đầu trò chơi ở đây
            SceneManager.LoadScene(1);
        }
        else
        {
            // Nếu chưa đăng nhập, hiển thị cảnh báo
            Debug.LogWarning("Please log in to start the game!");
            // Có thể sử dụng một UI Alert khác nếu cần
            // Ví dụ: sử dụng một Text để hiển thị cảnh báo
            notifyText.text = "Please log in to start the game!";
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

// Phương thức để tìm nút tương ứng với mỗi panel
private Button GetButtonForPanel(GameObject panel)
{
    if (panel == panels[0]) return accountManagerButton;
    if (panel == panels[1]) return settingButton;
    if (panel == panels[2]) return helpButton;
    if (panel == panels[3]) return creditButton;

    return null; // Trả về null nếu không tìm thấy
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

    public void RegisterAccout()
    {
        string email = emailInputRegister.text;
        string password = passwordInputRegister.text;
        string confirmpassword = confirmPasswordInputRegister.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Đăng ký thất bại: " + task.Exception);
                return;
            }
            if(task.IsCanceled)
            {
                Debug.LogError("Đăng ký bị từ chối: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                Debug.LogError("Đăng ký thành công");
            }
                

            FirebaseUser newUser = task.Result.User; // Sửa ở đây
            string displayName = newUser.Email.Split('@')[0]; // Lấy tên người chơi
            DisplayPlayerName(displayName);
        });
    }

    public void LoginAccout()
    {
        string email = emailInputLogIn.text;
        string password = passwordInputLogIn.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Đăng ký thất bại: " + task.Exception);
                return;
            }
            if (task.IsCanceled)
            {
                Debug.LogError("Đăng ký bị từ chối: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                Debug.LogError("Đăng ký thành công");
            }

            FirebaseUser newUser = task.Result.User; // Sửa ở đây
            string displayName = newUser.Email.Split('@')[0]; // Lấy tên người chơi
            DisplayPlayerName(displayName);
        });
    }
    private void DisplayPlayerName(string name)
    {
        playerNamePanel.SetActive(true);
        notifyText.text = $"Welcome, {name}!"; // Hiển thị tên người chơi
    }
}
