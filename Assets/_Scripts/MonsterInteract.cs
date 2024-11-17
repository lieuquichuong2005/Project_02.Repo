using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System;


public class MonsterInteract : MonoBehaviour
{
    public HealthBar healthBar;

    //public int current_health;
    public int min_randHealth;
    public int max_randHealth;
    public int current_health;

    public int min_damage;
    public int max_damage;
    public int damage;

    bool isDead = false;

    System.Random random = new System.Random();

    void Start()
    {
        current_health = random.Next(min_randHealth, max_randHealth);
        damage = random.Next(min_damage, max_damage);
        healthBar.SetMaxHealth(current_health);
    }

    void Update()
    {
        if (current_health < 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }


    public int GetDamage()
    {
        return damage;
    }

    public bool Status()
    {
        return isDead;
    }

    public void ReceiveDamage(int damage)
    {
        current_health -= damage;
        healthBar.SetHealth(current_health);
    }

    public void Revive()
    {
        current_health = random.Next(min_randHealth, max_randHealth);
        damage = random.Next(min_damage, max_damage);
        healthBar.SetMaxHealth(current_health);
        healthBar.SetHealth(current_health);
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
