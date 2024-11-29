using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Pathfinding;
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
    bool isAIenabled = false;

    bool isSlowedDown = false;
    float timer = 0f;

    bool isFrozen = false;
    float timer2 = 0f;

    float speed = 0f;

    System.Random random = new System.Random();

    void Start()
    {
        current_health = random.Next(min_randHealth, max_randHealth);
        damage = random.Next(min_damage, max_damage);
        healthBar.SetMaxHealth(current_health);
        speed = this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed;
    }

    void Update()
    {
        if (current_health < 0)
        {
            this.transform.parent.gameObject.SetActive(false);
            this.transform.parent.gameObject.GetComponent<Seeker>().enabled = false;
            this.transform.parent.gameObject.GetComponent<AIPath>().enabled = false;
            this.transform.parent.gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            isAIenabled = false;
        }

        if (isSlowedDown)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                UnSlowDown();
            }
        }

        if (isFrozen)
        {
            timer2 -= Time.deltaTime;

            if (timer2 <= 0f)
            {
                UnFreeze();
            }
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

    public bool AIStatus()
    {
        return isAIenabled;
    }

    public void ReceiveDamage(int damage)
    {
        current_health -= damage;
        healthBar.SetHealth(current_health);
        this.transform.parent.gameObject.GetComponent<Seeker>().enabled = true;
        this.transform.parent.gameObject.GetComponent<AIPath>().enabled = true;
        this.transform.parent.gameObject.GetComponent<AIDestinationSetter>().enabled = true;
        isAIenabled = true;
    }

    public void SlowDown()
    {
        isSlowedDown = true;
        timer = 3f;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed /= 2f;
    }

    public void UnSlowDown()
    {
        isSlowedDown = false;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed *= 2f;
    }

    public void Freeze()
    {
        isFrozen = true;
        timer2 = 4f;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 0f;
    }

    public void UnFreeze()
    {
        isFrozen = false;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = speed;
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
