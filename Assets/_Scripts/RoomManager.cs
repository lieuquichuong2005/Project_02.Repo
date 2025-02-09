using Photon.Pun;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    public TMP_Text nameRoom;

    public void JoinRoom()
    {
        //GameObject.Find("NetworkManager").GetComponent<CreateAndJoinSever>().JoinRoomInList(nameRoom.text);
        PhotonNetwork.JoinRoom(nameRoom.text);
    }    
}
