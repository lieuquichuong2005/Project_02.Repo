using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    //public static PlayerMovement instance;

    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;

    public float moveSpeed;
    public float weaponSpeed;
    private float moveX;
    private float moveY;

    float timeUntilWeapon;
    public GameObject WeaponRotate;

    //public List<PlayerItems> items = new List<PlayerItems>();

    void Awake()
    {

    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
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
                    rb2d.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
                    animator.SetFloat("MoveX", moveX);
                    animator.SetFloat("MoveY", moveY);
                    animator.speed = 1f; // Bật hoạt ảnh khi di chuyển

                    UpdateSprite(); // Cập nhật sprite theo hướng
                }
                else
                {
                    rb2d.linearVelocity = Vector2.zero; // Dừng di chuyển khi không có input   
                }
            }
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

    private void UpdateSprite()
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

        WeaponRotate.transform.rotation = Quaternion.Euler(rotationVector);
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
