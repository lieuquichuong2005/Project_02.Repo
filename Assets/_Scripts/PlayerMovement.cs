using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    PlayerStats playerStats;

    private SpriteRenderer sprite;
    public Sprite[] sprites;


    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;
    bool isMove;

    //public float weaponSpeed;
    private float moveX;
    private float moveY;


    //float timeUntilWeapon;

    //public GameObject WeaponRotate;

    //public List<PlayerItems> items = new List<PlayerItems>();


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int damage = 20;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);


        isMove = false;

        sprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (photonView.IsMine && isCanMove)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            //Debug.Log("moveX: " + moveX + "moveY: " + moveY);

            // Di chuyển và cập nhật hoạt ảnh
            if (moveX != 0 || moveY != 0)
            {
                animator.speed = 1f; // Bật hoạt ảnh khi di chuyển
                rb2d.linearVelocity = new Vector2(moveX * playerStats.moveSpeed, moveY * playerStats.moveSpeed);
                animator.SetFloat("MoveX", moveX);
                animator.SetFloat("MoveY", moveY);


                UpdateSprite(); // Cập nhật sprite theo hướng
            }
            else

            {
                animator.speed = 0f;
                rb2d.linearVelocity = Vector2.zero; // Dừng di chuyển khi không có input   

            }
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

    private void UpdateSprite()
    {
        var rotationVector = transform.rotation.eulerAngles;

        if (moveY > 0) // Hướng lên
        {
            rotationVector.z = 0f;
            sprite.sprite = sprites[0];
        }
        else if (moveY < 0) // Hướng xuống
        {
            rotationVector.z = 180f;
            sprite.sprite = sprites[1];
        }
        else if (moveX < 0) // Hướng trái
        {
            rotationVector.z = 90f;
            sprite.sprite = sprites[2];
        }
        else if (moveX > 0) // Hướng phải
        {
            rotationVector.z = -90f;
            sprite.sprite = sprites[3];
        }
    }


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
