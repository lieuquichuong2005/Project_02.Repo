using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CreateAndJoinSever : MonoBehaviourPunCallbacks
{
    public Button createRoom;
    public Button joinRoom;

    public TMP_InputField roomNameCreatedInput;
    public TMP_InputField roomNameJoinedInput;

    private void Awake()
    {
        createRoom.onClick.AddListener(OnCreateRoomButton);
        joinRoom.onClick.AddListener(OnJoinRoomButton);
    }

    void OnCreateRoomButton()
    {
        if (roomNameCreatedInput.text == null || roomNameCreatedInput.text == "")
        {
            Debug.LogWarning("Tên Phòng Không Thể Trống.");
            return;
        }
        RoomOptions roomOptions = new RoomOptions
        {
            IsOpen = true, 
            IsVisible = false, 
            MaxPlayers = 6 
        };
        PhotonNetwork.CreateRoom(roomNameCreatedInput.text, roomOptions);
    }

    void OnJoinRoomButton()
    {
        PhotonNetwork.JoinRoom(roomNameJoinedInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
}

