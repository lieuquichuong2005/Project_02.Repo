using Cinemachine;
using UnityEngine;

public class PositionAfterLoadScene : MonoBehaviour
{
    public GameObject player;
    public Vector2[] position;
    public int newScene;
    private CinemachineVirtualCamera cinemachineVirtual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineVirtual = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");
        if (PlayerMovement.currentScene == 3)
            player.transform.position = position[0];
        else if (PlayerMovement.currentScene == 5)
            player.transform.position = position[1];
        PlayerMovement.currentScene = newScene;
        cinemachineVirtual.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("CinemachineBound").GetComponent<PolygonCollider2D>();
    }

}
