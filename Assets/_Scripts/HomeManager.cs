using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Button startGame;
    public Button accountManager;
    public Button Setting;
    public Button Help;
    public Button credit;
    public Button quit;

    public GameObject accountPanel; // Panel cho đăng nhập/đăng ký
    public InputField usernameInput; // Trường nhập username
    public InputField passwordInput; // Trường nhập password
    public Text infoText; // Text hiển thị thông tin

    private void Awake()
    {
        
    }

    void Start()
    {
        // Ẩn panel quản lý tài khoản khi bắt đầu
        accountPanel.SetActive(false);
    }

    // Hàm gọi khi nhấn nút "Start"
    public void OnStartButton()
    {
        // Chuyển đến scene chơi game
        SceneManager.LoadScene("GameScene"); // Đổi tên thành scene của bạn
    }

    // Hàm gọi khi nhấn nút "Account Management"
    public void OnAccountManagementButton()
    {
        // Hiển thị panel tài khoản và ẩn menu chính
        accountPanel.SetActive(true);
    }

    // Hàm gọi khi nhấn nút "Settings"
    public void OnSettingsButton()
    {
        // Chỉ hiển thị thông báo chưa có tính năng này
        infoText.text = "Settings feature is not implemented yet.";
    }

    // Hàm gọi khi nhấn nút "Help"
    public void OnHelpButton()
    {
        // Chỉ hiển thị thông báo chưa có tính năng này
        infoText.text = "Help feature is not implemented yet.";
    }

    // Hàm gọi khi nhấn nút "Credits"
    public void OnCreditsButton()
    {
        // Chỉ hiển thị thông báo chưa có tính năng này
        infoText.text = "Credits feature is not implemented yet.";
    }

    // Hàm gọi khi nhấn nút "Quit"
    public void OnQuitButton()
    {
        Application.Quit(); // Thoát game
        Debug.Log("Game is quitting..."); // Ghi log cho kiểm tra
    }

    // Hàm gọi khi nhấn nút "Switch to Register"
    public void OnSwitchToRegister()
    {
        // Chuyển đổi giữa đăng nhập và đăng ký
        infoText.text = "Switch to Register functionality will be implemented.";
    }

    // Hàm gọi khi nhấn nút "Login with Facebook"
    public void OnLoginWithFacebook()
    {
        // Logic để đăng nhập bằng Facebook (cần tích hợp SDK Facebook)
        infoText.text = "Login with Facebook functionality will be implemented.";
    }

    // Hàm gọi khi nhấn nút "Login with Email"
    public void OnLoginWithEmail()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Kiểm tra thông tin đăng nhập (giả định đơn giản)
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            infoText.text = "Please enter username and password.";
        }
        else
        {
            // Logic để xử lý đăng nhập
            infoText.text = "Logged in successfully!"; // Thay đổi logic ở đây
            // Quay lại menu chính
            accountPanel.SetActive(false);
        }
    }
}