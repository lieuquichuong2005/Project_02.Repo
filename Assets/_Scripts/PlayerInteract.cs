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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            int damage = collision.gameObject.GetComponent<MonsterInteract>().GetDamage();

            current_health -= damage;
            healthBar.SetHealth(current_health);
        }
    }
}
