using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject cameraPrefab;
    void Awake()
    {
        // Kiểm tra xem đã có player nào không
        if (GameObject.FindWithTag("Player") == null)
        {
            GameObject player = PhotonNetwork.Instantiate("Player_Knight", new Vector3(Random.Range(-8, 0), Random.Range(-3, -6), 0), Quaternion.identity);
            CreateCameraForPlayer(player);
        }
        else
        {
            Debug.Log("Player already exists.");
        }
    }

    private void CreateCameraForPlayer(GameObject player)
    {
        if (GameObject.FindWithTag("Cinemachine") == null)
        {
            GameObject cameraInstance = Instantiate(cameraPrefab);
            DontDestroyOnLoad(cameraInstance);
            var cinemachineCamera = cameraInstance.GetComponent<CinemachineVirtualCamera>();
            cinemachineCamera.tag = "Cinemachine";
            cinemachineCamera.Follow = player.transform;
            cinemachineCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.FindWithTag("CinemachineBound").GetComponent<PolygonCollider2D>();
        }
    }
}
