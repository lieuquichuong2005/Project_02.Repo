using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
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

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            current_health -= 10;
            healthBar.SetHealth(current_health);
        }
    }
}