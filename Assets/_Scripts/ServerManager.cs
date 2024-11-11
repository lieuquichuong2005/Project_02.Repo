using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth; // Nếu bạn sử dụng Firebase

public class ServerManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance; // Khởi tạo Firebase Auth
        CheckAccountStatus();
    }

    private void CheckAccountStatus()
    {
        if (auth.CurrentUser != null)
        {
            // Kiểm tra xem người dùng đã có nhân vật hay chưa
            bool hasCharacter = CheckIfUserHasCharacter(auth.CurrentUser.UserId);
            if (hasCharacter)
            {
                // Nếu đã có nhân vật, chuyển đến PlayScene
                SceneManager.LoadScene("PlayScene");
            }
            else
            {
                // Nếu chưa có nhân vật, chuyển đến Character Selection Scene
                SceneManager.LoadScene("CharacterSelectionScene");
            }
        }
        else
        {
            // Nếu không có người dùng, có thể quay lại Home Scene
            SceneManager.LoadScene("HomeScene");
        }
    }

    private bool CheckIfUserHasCharacter(string userId)
    {
        // Logic kiểm tra xem người dùng đã có nhân vật hay chưa
        // Bạn có thể sử dụng database để kiểm tra
        // Ví dụ giả định: trả về false nếu chưa có nhân vật
        return false; // Thay đổi logic phù hợp với bạn
    }
}