using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    //public int sceneToLoad;
    public string sceneToLoad;
    public int level_limit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int current_level = collision.gameObject.GetComponent<PlayerStats>().GetLevel();
            if (current_level >= level_limit)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Not high enough level");
            }
        }
    }
}