using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Cinemachine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class Skill_Warrior : MonoBehaviour
{
    //--------------- Skill 1: Slash -----------------------
    //Nhấn phím 1, player sẽ dịch chuyển đến enemy gần nhất và chém. Tạo 100 damage


    private Rigidbody2D rb2d;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    public bool isCanMove = true;

    // Skill 1 params
    public int slash_damage = 100;

    // Skill 3 params
    public GameObject circularWeapon;
    public int circular_damage = 50;
    public float set_timer = 5f;
    public float timer = 0f;
    public float rotate_speed = 60f;
    public bool skill3Activated = false;
    public Transform player_obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        circularWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (isCanMove)
            {
                // Skill 1
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    if (enemies.Length != 0)
                    {
                        float distance = 1000f;
                        GameObject chosenObj = null;
                        foreach (GameObject enemy in enemies)
                        {
                            float cur_distance = Vector3.Distance(this.transform.position, enemy.transform.position);
                            if (cur_distance < distance)
                            {
                                distance = cur_distance;
                                chosenObj = enemy;
                            }
                        }
                        
                        this.transform.position = chosenObj.transform.position;
                        animator.SetTrigger("Skill1");
                    }
                    else
                    {
                        Debug.Log("No enemy found");
                    }
                }

                // Skill 3
                if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && skill3Activated == false)
                {
                    skill3Activated = true;
                    timer = set_timer;
                    circularWeapon.SetActive(true);
                }

                if (skill3Activated)
                {
                    timer -= Time.deltaTime;
                    circularWeapon.transform.RotateAround(player_obj.position, Vector3.forward, rotate_speed);
                    //circularWeapon.transform.rotation = Quaternion.Euler(0f, 0f, rotate_speed * Time.deltaTime);

                    if (timer <= 0f)
                    {
                        circularWeapon.SetActive(false);
                        skill3Activated = false;
                    }
                }
            }
        }
    }

    public void Slash()
    {
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitenemies)
        {
            if (enemy.tag == "enemy")
            {
                Debug.Log("Hit");
                enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(slash_damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            collider.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(circular_damage);
            collider.gameObject.GetComponent<MonsterInteract>().SlowDown();
        }
    }
}
