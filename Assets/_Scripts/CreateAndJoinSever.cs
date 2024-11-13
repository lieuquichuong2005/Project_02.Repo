using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
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
        PhotonNetwork.CreateRoom(roomNameCreatedInput.text);
    }

    void OnJoinRoomButton()
    {
        PhotonNetwork.JoinRoom(roomNameJoinedInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
    public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}

