using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviourPunCallbacks
{
    // Mảng chứa hình ảnh nhân vật
    public Sprite[] characterSprites;
    // Đối tượng Image để hiển thị nhân vật
    public Image characterImage;
    private int currentIndex = 0;

    public Button confirmButton;
    public Button nextButton;
    public Button previousButton;

    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmSelection);
        nextButton.onClick.AddListener(NextCharacter);
        previousButton.onClick.AddListener(PreviousCharacter); 
        UpdateDisplay(); // Cập nhật hình ảnh ban đầu
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characterSprites.Length; // Chuyển đến nhân vật tiếp theo
        UpdateDisplay();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characterSprites.Length) % characterSprites.Length; // Quay lại nhân vật trước
        UpdateDisplay();
    }

    public void ConfirmSelection()
    {
        Debug.Log($"Bạn đã chọn nhân vật thứ {currentIndex + 1}"); // Xác nhận nhân vật đã chọn
        // Có thể thêm hành động khác ở đây
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex); // Lưu lựa chọn nhân vật
        SceneManager.LoadScene(2); // Chuyển sang GameScene
    }

    private void UpdateDisplay()
    {
        characterImage.sprite = characterSprites[currentIndex]; // Cập nhật hình ảnh nhân vật
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new Photon.Realtime.RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity, 0);
    }
}