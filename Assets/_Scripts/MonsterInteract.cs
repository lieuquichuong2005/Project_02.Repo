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

    //public int current_health;
    public int min_randHealth;
    public int max_randHealth;
    public int current_health;

    public int min_damage;
    public int max_damage;
    public int damage;

    public int min_exp;
    public int max_exp;
    public int exp;

    bool isDead = false;
    float timeAnim = 0.5f;
    bool isAIenabled = false;

    bool isSlowedDown = false;
    float timer = 0f;

    bool isFrozen = false;
    float timer2 = 0f;

    float speed = 0f;

    System.Random random = new System.Random();

    void Start()
    {
        deadAnim.SetActive(false);
        current_health = random.Next(min_randHealth, max_randHealth);
        damage = random.Next(min_damage, max_damage);
        exp = random.Next(min_exp, max_exp);
        healthBar.SetMaxHealth(current_health);
        speed = this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed;
        SlowEffect.SetActive(false);
        StunEffect.SetActive(false);
    }

    void Update()
    {
        //Debug.Log(deadAnim.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (current_health <= 0)
        {
            deadAnim.SetActive(true);
            this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 0f;
            //Debug.Log("Error 1");
            //StartCoroutine(WaitDeadAnim());
            //Debug.Log("Error 5");
            //this.transform.parent.gameObject.SetActive(false);
            /*this.transform.parent.gameObject.GetComponent<Seeker>().enabled = false;
            this.transform.parent.gameObject.GetComponent<AIPath>().enabled = false;
            this.transform.parent.gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            isAIenabled = false;*/

            timeAnim -= Time.deltaTime;
            if (timeAnim <= 0f)
            {
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
                SlowEffect.SetActive(false);
                UnSlowDown();
            }
        }

        if (isFrozen)
        {
            timer2 -= Time.deltaTime;

            if (timer2 <= 0f)
            {
                StunEffect.SetActive(false);
                UnFreeze();
            }
        }
    }

    /*IEnumerator WaitDeadAnim()
    {
        Debug.Log("Error 2");
        Animator anim = deadAnim.gameObject.GetComponent<Animator>();
        anim.Play("dead");
        yield return new WaitForSeconds(5);
        Debug.Log("Error 3");
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("dead") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            Debug.Log("Error 4");
            yield return null;
        }
    }*/

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
        isSlowedDown = false;
        this.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed *= 2f;
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
