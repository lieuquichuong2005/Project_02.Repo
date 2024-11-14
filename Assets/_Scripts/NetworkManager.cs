using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject lobbyPanel;
    public GameObject loadingServerPanel;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /*public override void OnDisconnected(DisconnectCause cause)
    {
        // Sau khi ngắt kết nối, bạn có thể kết nối lại nếu muốn
        PhotonNetwork.ConnectUsingSettings();
    }*/
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        loadingServerPanel.gameObject.SetActive(false);
        lobbyPanel.gameObject.SetActive(true);
    }
}