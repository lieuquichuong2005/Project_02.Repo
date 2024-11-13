using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            GameObject room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            room.GetComponent<Room>().nameRoom.text = roomList[i].Name;
        }
    }

}

