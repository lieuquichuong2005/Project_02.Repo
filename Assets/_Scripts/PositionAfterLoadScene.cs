using Cinemachine;
using UnityEngine;

public class PositionAfterLoadScene : MonoBehaviour
{
    public GameObject player;
    public Vector2[] position; // Thiết lập trong Inspector
    public string thisSceneName;
    public CinemachineVirtualCamera cinemachineVirtual;
    public string[] oldScene; // Thiết lập trong Inspector

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cinemachineVirtual = GameObject.FindWithTag("Cinemachine")?.GetComponent<CinemachineVirtualCamera>();

        if (cinemachineVirtual == null)
        {
            Debug.LogError("Cinemachine camera not found!");    
            return;
        }

        cinemachineVirtual.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("CinemachineBound").GetComponent<PolygonCollider2D>();     
        cinemachineVirtual.Follow = player.transform;

        // Kiểm tra chỉ số trước khi truy cập
        for (int i = 0; i < oldScene.Length; i++)
        {
            if (PlayerMovement.currentScene == oldScene[i])
            {
                if (i < position.Length) // Kiểm tra chỉ số vị trí
                {
                    player.transform.position = position[i];
                }
                break; // Thoát vòng lặp khi tìm thấy chỉ số
            }
        }

        PlayerMovement.currentScene = thisSceneName;
    }
}