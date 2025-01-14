using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    //public int sceneToLoad;
    public string sceneToLoad;
    public int level_limit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int isTester = PlayerPrefs.GetInt("isTester");
        Debug.Log(isTester);
        if (collision.gameObject.tag == "Player")
        {
            int current_level = collision.gameObject.GetComponent<PlayerStats>().GetLevel();
            if (current_level >= level_limit || isTester == 1)
            {
                if (sceneToLoad != "")
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                Debug.Log("Not high enough level");
            }
        }
    }
}