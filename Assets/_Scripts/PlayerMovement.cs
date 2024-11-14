using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    //public static PlayerMovement instance;

    private SpriteRenderer sprite;
    public Sprite[] sprites;

    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;

    public float moveSpeed;
    public float weaponSpeed;
    private float moveX;
    private float moveY;

    float timeUntilWeapon;
    //public GameObject WeaponRotate;

    //public List<PlayerItems> items = new List<PlayerItems>();

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (isCanMove)
            {
                moveSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 7f : 3f;
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                // Di chuyển và cập nhật hoạt ảnh
                if (moveX != 0 || moveY != 0)
                {
                    animator.speed = 1f; // Bật hoạt ảnh khi di chuyển
                    rb2d.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
                    animator.SetFloat("MoveX", moveX);
                    animator.SetFloat("MoveY", moveY);

                    UpdateSprite(); // Cập nhật sprite theo hướng
                }
                else
                {
                    animator.speed = 0f;
                    rb2d.linearVelocity = Vector2.zero; // Dừng di chuyển khi không có input   
                }
                if (timeUntilWeapon <= 0f)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        animator.SetTrigger("Attack");
                        timeUntilWeapon = weaponSpeed;
                    }
                }
                else
                {
                    timeUntilWeapon -= Time.deltaTime;
                }
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
        //WeaponRotate.transform.rotation = Quaternion.Euler(rotationVector);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "enemy")
        {
            Debug.Log("Hit");
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
}
