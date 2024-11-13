using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public GameObject[] allRooms;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < allRooms.Length; i++)
        {
            if (allRooms[i] == null)
                Destroy(allRooms[i]);
        }
        allRooms = new GameObject[roomList.Count];

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                room.GetComponent<Room>().nameRoom.text = roomList[i].Name;

                allRooms[i] = room;
            }
        }
    }

}

