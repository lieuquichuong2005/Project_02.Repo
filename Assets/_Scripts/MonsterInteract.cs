using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Pathfinding;
using System;


public class MonsterInteract : MonoBehaviour
{
    public GameObject deadAnim;

    public HealthBar healthBar;

    public GameObject SlowEffect;
    public GameObject StunEffect;

    public int min_randHealth;
    public int max_randHealth;
    public int current_health;

    public int min_damage;
    public int max_damage;
    public int damage;

    public int min_exp;
    public int max_exp;
    public int exp;

    public GameObject coinPrefab;
    public int min_coin;
    public int max_coin;
    public int coin;
    public bool isCreateCoin;

    bool isDead = false;
    float timeAnim = 0.5f;
    bool isAIenabled = false;

    bool isSlowedDown = false;
    float timer = 0f;

    bool isFrozen = false;
    float timer2 = 0f;

    bool isAttacking = false;
    float attackTimer = 1f;

    float speed = 0f;

    System.Random random = new System.Random();

    void Start()
    {
        deadAnim.SetActive(false);
        current_health = random.Next(min_randHealth, max_randHealth + 1);
        damage = random.Next(min_damage, max_damage + 1);
        exp = random.Next(min_exp, max_exp + 1);
        coin = random.Next(min_coin, max_coin + 1);
        healthBar.SetMaxHealth(current_health);
        speed = this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed;
        SlowEffect.SetActive(false);
        StunEffect.SetActive(false);
        isCreateCoin = false;
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
        }

        if (current_health <= 0)
        {
            if (!isCreateCoin)
            {
                for (int i = 1; i <= coin; i++)
                {
                    Vector3 last_current_pos = this.transform.parent.gameObject.transform.position;
                    Instantiate(coinPrefab, new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f) + last_current_pos.x, UnityEngine.Random.Range(-1.5f, 1.5f) + last_current_pos.y, 0f), Quaternion.identity);
                }
                isCreateCoin = true;
            }

            deadAnim.SetActive(true);
            this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 0f;

            timeAnim -= Time.deltaTime;
            if (timeAnim <= 0f)
            {
                UnFreeze();
                UnSlowDown();
                this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = speed;
                this.transform.parent.gameObject.GetComponent<Seeker>().enabled = false;
                this.transform.parent.gameObject.GetComponent<AIPath>().enabled = false;
                this.transform.parent.gameObject.GetComponent<AIDestinationSetter>().enabled = false;
                isAIenabled = false;
                this.transform.parent.gameObject.SetActive(false);
                deadAnim.SetActive(false);
            }
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

    public int GetHealth()
    {
        return current_health;
    }

    public int GetEXP()
    {
        return exp;
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
        SlowEffect.SetActive(true);
        isSlowedDown = true;
        timer = 3f;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed /= 2f;
    }

    public void UnSlowDown()
    {
        SlowEffect.SetActive(false);
        isSlowedDown = false;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = speed;
    }

    public void Freeze()
    {
        StunEffect.SetActive(true);
        isFrozen = true;
        timer2 = 4f;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 0f;
    }

    public void UnFreeze()
    {
        StunEffect.SetActive(false);
        isFrozen = false;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = speed;
    }

    public void Revive()
    {
        current_health = random.Next(min_randHealth, max_randHealth);
        damage = random.Next(min_damage, max_damage);
        healthBar.SetMaxHealth(current_health);
        healthBar.SetHealth(current_health);
        timeAnim = 0.5f;
        isCreateCoin = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = true;
            Debug.Log("attacked player");
            collision.gameObject.GetComponent<PlayerStats>().EarnDamage(damage);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && attackTimer <= 0f)
        {
            Debug.Log("continue attacked player");
            collision.gameObject.GetComponent<PlayerStats>().EarnDamage(damage);
            attackTimer = 1f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("stop attack");
            isAttacking = false;
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
