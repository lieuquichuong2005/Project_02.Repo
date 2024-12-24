using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
    public GameObject shopPanel;
    private void Start()
    {
        shopPanel.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            shopPanel.SetActive(true);
    }
}
