using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MonsterInteract : MonoBehaviour
{
    public HealthBar healthBar;
    int current_health;

    void Start()
    {
        current_health = 100;
        healthBar.SetMaxHealth(100);
    }

    void Update()
    {
        if (current_health == 0)
        {
            Debug.Log("Dead");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            current_health -= 5;
            healthBar.SetHealth(current_health);
        }

        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "weapon")
        {
            current_health -= 20;
            healthBar.SetHealth(current_health);
        }
    }
}
