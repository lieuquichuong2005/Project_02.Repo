using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject cameraPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("Player-Knight", new Vector3(Random.Range(-8, 0), Random.Range(-3, -6), 0), Quaternion.identity);
        CreateCameraForPlayer(player);
    }

    private void CreateCameraForPlayer(GameObject player)
    {
        GameObject cameraInstance = Instantiate(cameraPrefab);
        DontDestroyOnLoad(cameraInstance);
        var cinemachineCamera = cameraInstance.GetComponent<CinemachineVirtualCamera>();
        cinemachineCamera.tag = "Cinemachine";
        cinemachineCamera.Follow = player.transform;
        cinemachineCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("CinemachineBound").GetComponent<PolygonCollider2D>();
    }
}
