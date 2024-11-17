using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    void Update()
    {
        if (photonView.IsMine) // Chỉ cho phép người chơi kiểm soát nhân vật của mình
        {
            Move();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        transform.position += movement * Time.deltaTime;
    }
}