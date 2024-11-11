using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Kết nối đến Photon
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // Tham gia lobby
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 }; // Số người chơi tối đa
        PhotonNetwork.CreateRoom(roomName, roomOptions); // Tạo phòng mới
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName); // Tham gia phòng
    }
}