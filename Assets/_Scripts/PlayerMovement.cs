﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement instance;

    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;

    public float moveSpeed;
    private float moveX;
    private float moveY;

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

    private void UpdateSprite()
    {
        if (moveY > 0) // Hướng lên
        {
            animator.SetFloat("MoveY", 0);
        }
        else if (moveY < 0) // Hướng xuống
        {
            animator.SetFloat("MoveY", 0);
        }
        else if (moveX < 0) // Hướng trái
        {
            animator.SetFloat("MoveX", 0);
        }
        else if (moveX > 0) // Hướng phải
        {
            animator.SetFloat("MoveX", 0);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Items"))
        {
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
