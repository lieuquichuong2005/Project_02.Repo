using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Cinemachine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    public static PlayerMovement instance;

    public static int currentScene;

    [SerializeField] GameObject[] itemButton;

    public GameObject playerStats;
    public PlayerCollider playerCollider;

    public GameObject playerInformationPanel;
    //public CinemachineVirtualCamera virtualCamera;
    public GameObject marker;

    private Rigidbody2D rb2d;
    public Animator animator;

    public bool isCanMove = true;

    float moveSpeed = 3;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int damage = 20;


    void Awake()
    {
        if(instance == null)
            instance = this;
        else 
            Destroy(instance);
        //playerStats = GameObject.FindWithTag("PlayerStats");
        //playerCollider = GetComponent<PlayerCollider>();
        //marker = GameObject.FindWithTag("PlayerMarker");
        marker.SetActive(true);
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        DontDestroyOnLoad (this.gameObject);
        //DontDestroyOnLoad(playerStats.gameObject);
        currentScene = 3;
        playerInformationPanel.gameObject.SetActive(false);
        Debug.Log(currentScene);
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (isCanMove)
            {
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");
                    animator.SetFloat("MoveX", moveX);
                    animator.SetFloat("MoveY", moveY);
                    rb2d.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);

                    //UpdateSprite(); // Cập nhật sprite theo hướng

                if (Input.GetKeyDown(KeyCode.Space))
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
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    playerInformationPanel.SetActive(!playerInformationPanel.activeSelf);
                    ShowItemInInventory();
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
    void ShowItemInInventory()
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            var oneItemButton = itemButton[i];
            oneItemButton.transform.GetChild(0).gameObject.SetActive(false);
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = " ";
        }

        var items = playerCollider.itemInventory;
        for (int i = 0; i < items.Count; i++)
        {
            var oneItemButton = items[i];
            oneItemButton.transform.GetChild(0).gameObject.SetActive(true);
            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = items[i].item.itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = items[i].quanlity.ToString();
        }
    }    
}
