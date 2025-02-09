using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;
public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public Transform roomTransformPos;
    public GameObject[] allRooms;

    private void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Số phòng đang có: " + roomList.Count);
        if (allRooms == null)
        {
            allRooms = new GameObject[0];
        }
        for (int i = 0; i < allRooms.Length; i++)
            {
                if (allRooms[i] != null)
                    Destroy(allRooms[i]);
        }

        allRooms = new GameObject[roomList.Count];

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, roomTransformPos);
                room.GetComponent<Room>().nameRoom.text = roomList[i].Name;

                allRooms[i] = room;
            }
        }
        foreach (var room in roomList)
        {
            Debug.Log($"Room: {room.Name}, IsOpen: {room.IsOpen}, IsVisible: {room.IsVisible}, PlayerCount: {room.PlayerCount}");
        }
    }
    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

}

