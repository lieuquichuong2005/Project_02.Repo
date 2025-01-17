using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Cinemachine;
//using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class Skill_Warrior : MonoBehaviour
{
    //--------------- Skill 1: Slash -----------------------
    //Nhấn phím 1, player sẽ dịch chuyển đến enemy gần nhất và chém. Tạo 100 damage

    //--------------- Skill 2: Tremor ----------------------
    //Nhấn phím 2, player đánh kiếm xuống và spawn các kiếm nhỏ tấn công từ dưới lên. Tạo 30 damage mỗi lần và stun enemy 4s

    //--------------- Skill 3: Circle ----------------------
    //Nhấn phím 3, 1 thanh kiếm xuất hiện xoay quanh player trong 5s, tạo 20 damage và làm chậm enemy 3s
    public PlayerMovement playerMovement;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    // Skill 1 params
    public int slash_damage = 100;

    // Skill 2 params
    public int tremor_damage = 30;
    public GameObject knifeTremorPrefab;
    public float set_timer = 2f;
    public float timer = 0f;
    public int numofprefab = 6;
    public bool skill2Activated = false;

    // Skill 3 params
    public GameObject circularWeapon;
    public int circular_damage = 20;
    public float set_timer2 = 5f;
    public float timer2 = 0f;
    public float rotate_speed;
    public bool skill3Activated = false;
    public Transform player_obj;

    bool skill2Trigger = false;
    bool skill3Trigger = false;

    public PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circularWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (PlayerMovement.isCanMove)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    PlayerMovement.isCanMove = true; 
                }
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
                        playerMovement.animator.SetTrigger("Skill_01");
                    }
                    else
                    {
                        Debug.Log("No enemy found");
                    }
                }

                // Skill 2
                if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)))
                {
                    skill2Trigger = true;
                    playerMovement.animator.SetTrigger("Skill_02");
                    timer = set_timer;
                    PlayerMovement.isCanMove = false;
                }

                // Skill 3
                if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && skill3Activated == false)
                {
                    skill3Trigger = true;
                    skill3Activated = true;
                    timer2 = set_timer2;
                    circularWeapon.SetActive(true);
                }

                if (skill2Activated)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        GameObject[] knives = GameObject.FindGameObjectsWithTag("warrior_skill2");
                        foreach (GameObject knife in knives)
                        {
                            if (knife.gameObject.transform.GetChild(0).gameObject.GetComponent<WarriorKnifeScript>().ReturnState())
                            {
                                GameObject enemyHit = knife.gameObject.transform.GetChild(0).gameObject.GetComponent<WarriorKnifeScript>().ReturnObj();
                                if (enemyHit.gameObject.GetComponent<MonsterInteract>().GetHealth() <= 0)
                                {
                                    int exp = enemyHit.gameObject.GetComponent<MonsterInteract>().GetEXP();
                                    playerStats.GainExperience(exp);
                                }
                            }
                            Destroy(knife);
                        }
                        skill2Activated = false;
                        skill2Trigger = false;
                    }
                }

                if (skill3Activated)
                {
                    timer2 -= Time.deltaTime;
                    circularWeapon.transform.RotateAround(player_obj.position, Vector3.forward, rotate_speed);
                    //circularWeapon.transform.rotation = Quaternion.Euler(0f, 0f, rotate_speed * Time.deltaTime);

                    if (timer2 <= 0f)
                    {
                        circularWeapon.SetActive(false);
                        skill3Activated = false;
                        skill3Trigger = false;
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
                enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(slash_damage);
                if (enemy.gameObject.GetComponent<MonsterInteract>().GetHealth() <= 0)
                {
                    int exp = enemy.gameObject.GetComponent<MonsterInteract>().GetEXP();
                    playerStats.GainExperience(exp);
                }
            }
        }
    }

    public void Tremor()
    {
        float x = playerMovement.animator.GetFloat("MoveX");
        float y = playerMovement.animator.GetFloat("MoveY");

        if (y == 0f && x > 0f) // Phải
        {
            for (float i = 1f; i <= 6f; i = i + 1f)
            {
                Instantiate(knifeTremorPrefab, player_obj.position + new Vector3(i, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            }
            skill2Activated = true;
            //Debug.Log("Phải");
            return;
        }

        if (y == 0f && x < 0f) // Trái
        {
            for (float i = 1f; i <= 6f; i = i + 1f)
            {
                Instantiate(knifeTremorPrefab, player_obj.position + new Vector3(-i, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            }
            skill2Activated = true;
            //Debug.Log("Trái");
            return;
        }

        if (y > 0f && x == 0f) // Trên
        {
            for (float i = 1f; i <= 5f; i = i + 1f)
            {
                Instantiate(knifeTremorPrefab, player_obj.position + new Vector3(0f, i, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            }
            skill2Activated = true;
            //Debug.Log("Trên");
            return;
        }

        if (y < 0f && x == 0f) // Dưới
        {
            for (float i = 1f; i <= 5f; i = i + 1f)
            {
                Instantiate(knifeTremorPrefab, player_obj.position + new Vector3(0f, -i, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            }
            skill2Activated = true;
            //Debug.Log("Dưới");
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {

            if (skill3Trigger)
            {
                collider.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(circular_damage);
                collider.gameObject.GetComponent<MonsterInteract>().SlowDown();
                if (collider.gameObject.GetComponent<MonsterInteract>().GetHealth() <= 0)
                {
                    int exp = collider.gameObject.GetComponent<MonsterInteract>().GetEXP();
                    playerStats.GainExperience(exp);
                }
            }
        }
    }
}
