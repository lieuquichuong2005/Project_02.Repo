using UnityEngine;
using System.Collections;

public class WarriorKnifeScript : MonoBehaviour
{
    //public Collider2D collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("skill 2 triggered");
            collision.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(30);
            collision.gameObject.GetComponent<MonsterInteract>().Freeze();
        }
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag == "enemy")
        {
            Debug.Log("skill 2 triggered");
            collider.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(30);
            collider.gameObject.GetComponent<MonsterInteract>().Freeze();
        }
    }
}
