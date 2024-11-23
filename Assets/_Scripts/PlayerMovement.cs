using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Cinemachine;

public class PlayerMovement : MonoBehaviourPun
{
    public static int currentScene;

    public GameObject playerStats;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject marker;

    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;

    float moveSpeed = 3;

    //public float weaponSpeed;
    //private float moveX;
    //private float moveY;

    //float timeUntilWeapon;
    //public GameObject WeaponRotate;

    //public List<PlayerItems> items = new List<PlayerItems>();

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int damage = 20;

    void Awake()
    {
        playerStats = GameObject.FindWithTag("PlayerStats");
        marker = GameObject.FindWithTag("PlayerMarker");
        marker.SetActive(true);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        DontDestroyOnLoad (this.gameObject);
        DontDestroyOnLoad(playerStats.gameObject);
        currentScene = 3;
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (isCanMove)
            {
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");
                if (moveX == 0f && moveY == 0f)
                {
                    animator.SetBool("isMove", false);
                }
                else
                {
                    animator.SetFloat("moveX", moveX);
                    animator.SetFloat("moveY", moveY);
                    rb2d.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
                    animator.SetBool("isMove", true);
                }

                    //UpdateSprite(); // Cập nhật sprite theo hướng

                if (Input.GetKey(KeyCode.Space))
                {
                    animator.SetBool("isAttack", true);
                    /*Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                    foreach(Collider2D enemy in hitenemies)
                    {
                        if (enemy.tag == "enemy")
                        {
                            Debug.Log("Hello");
                            enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(damage);
                            break;
                        }
                    }*/
                }
            }
        }
    }
    /*private void UpdateSprite()
    {
        var rotationVector = transform.rotation.eulerAngles;

        if (moveY > 0) // Hướng lên
        {

            animator.SetFloat("MoveY", 0);
            rotationVector.z = 0f;
        }
        else if (moveY < 0) // Hướng xuống
        {
            animator.SetFloat("MoveY", 0);
            rotationVector.z = 180f;
        }
        else if (moveX < 0) // Hướng trái
        {
            animator.SetFloat("MoveX", 0);
            rotationVector.z = 90f;
        }
        else if (moveX > 0) // Hướng phải
        {
            animator.SetFloat("MoveX", 0);
            rotationVector.z = -90f;
        }

    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "enemy")
        {
            Debug.Log("Hit");

        }
    }

    public void EndAttack()
    {
        animator.SetBool("isAttack", false);
    }

    public void Attack()
    {
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitenemies)
        {
            if (enemy.tag == "enemy")
            {
                Debug.Log("Hit");
                enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(damage);
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Items"))
        
            var item = other.gameObject.GetComponent<GameItem>();

            var check = items.Find(x => x.item.itemID == item.itemID);

            if (check == null)
                items.Add(new PlayerItems { item = item, quantity = 1 });
            else
            {
                check.quantity += 1;
                Debug.Log("Cộng Số Lượng");
            }

            Destroy(other.gameObject);
        }
    }*/

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    
}
