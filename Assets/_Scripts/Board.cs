using UnityEngine;

public class Board : MonoBehaviour
{
    public bool isActivated;
    public int boardNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isActivated = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collision.gameObject.CompareTag("Player"))
        {
            this.isActivated = true;
        }
    }

    public bool GetStatus()
    {
        return this.isActivated;
    }

    public void Deactivate()
    {
        this.isActivated = false;
    }
}
