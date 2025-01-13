using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class FirebareManager : MonoBehaviour
{
    public static FirebareManager Instance;
    public FirebaseAuth auth;
    private DatabaseReference dbReference;

    public HomeManager homeManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        // Khởi tạo Firebase
        auth = FirebaseAuth.DefaultInstance;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
        // Kiểm tra xem auth đã được khởi tạo chưa
        if (auth == null)
        {
            Debug.LogError("FirebaseAuth has not been initialized.");
        }
        InitializeFirebase();
    }

    public void InitializeFirebase()
    {
        Firebase.FirebaseApp.LogLevel = Firebase.LogLevel.Debug;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to initialize Firebase: " + task.Exception);
            }
            else
            {
                // Khởi tạo FirebaseAuth
                auth = FirebaseAuth.DefaultInstance;
                if (auth == null)
                {
                    Debug.LogError("FirebaseAuth has not been initialized.");
                }
            }
        });
    }
    public void RegisterAccout()
    {
        string email = homeManager.emailInputRegister.text;
        string password = homeManager.passwordInputRegister.text;
        string confirmpassword = homeManager.confirmPasswordInputRegister.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
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
                Debug.Log("Đăng ký thành công");
                FirebaseUser newUser = task.Result.User;
                homeManager.displayName = newUser.Email.Split('@')[0];
                homeManager.DisplayPlayerName(homeManager.displayName);

                homeManager.logOutButton.gameObject.SetActive(true);

            }


        });
    }
    public void LoginAccout()
    {
        string email = homeManager.emailInputLogIn.text;
        string password = homeManager.passwordInputLogIn.text;

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
                Debug.Log("Đăng ký thành công");
                FirebaseUser newUser = task.Result.User;
                homeManager.displayName = newUser.Email.Split('@')[0];
                homeManager.DisplayPlayerName(homeManager.displayName);

                homeManager.logOutButton.gameObject.SetActive(true);

            }

        });
    }
    public void LogOutAccount()
    {
        auth.SignOut(); // Đăng xuất khỏi Firebase
        homeManager.displayName = null; // Xóa tên tài khoản
        homeManager.nameTextPanel.gameObject.SetActive(false);
        homeManager.accountNameText.gameObject.SetActive(false); // Ẩn tên tài khoản
        homeManager.logOutButton.gameObject.SetActive(false); // Ẩn nút đăng xuất
        Debug.Log("Logged out successfully.");
        homeManager.emailInputLogIn.text = null;

        homeManager.passwordInputLogIn.text = null;
    }
    public void SavePlayerData(string playerId, string playerName, int level, int exp, int gold, string[] items)
    {
        PlayerData data = new PlayerData(playerId, playerName, level, exp, gold, items);
        string json = JsonUtility.ToJson(data);

        dbReference.Child("players").Child(playerId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Player data saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save player data.");
            }
        });
    }
    public void LoadPlayerData(string playerId)
    {
        dbReference.Child("players").Child(playerId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error loading data.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData data = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                Debug.Log($"ID: {data.id}, Name: {data.name}, Level: {data.level}, Exp: {data.exp}, Gold: {data.gold}, Items: {string.Join(", ", data.items)}");
            }
        });
    }
}
