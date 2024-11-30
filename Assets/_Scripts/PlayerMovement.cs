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

    public static string currentScene;

    [SerializeField] GameObject[] itemButton;
    public GameObject playerInformationPanel;
    public GameObject marker;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteDirection;
    public PlayerStats playerStats;
    public PlayerCollider playerCollider;

    private Rigidbody2D rb2d;
    public Animator animator;


    public LayerMask enemyLayers;
    public Transform attackPoint;
    public bool isCanMove = true;
    public float attackRange = 0.5f;
    private string direction;
    float moveX;
    float moveY;

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
        animator = GetComponent<Animator>();
        //animator = GetComponent<Animator>();

        DontDestroyOnLoad (this.gameObject);
        //DontDestroyOnLoad(playerStats.gameObject);
        currentScene = "Làng Tân Thủ";
        playerInformationPanel.gameObject.SetActive(false);
        Debug.Log(currentScene);
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (isCanMove)
            {
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                if(moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
                {
                    animator.SetFloat("LastMoveX", moveX);
                    animator.SetFloat("LastMoveY", moveY) ;
                }
            }
            else
            {
                moveX = 0;
                moveY = 0;
            } 
                rb2d.linearVelocity = new Vector2(moveX * playerStats.moveSpeed, moveY * playerStats.moveSpeed);
                
                animator.SetFloat("MoveX", moveX);
                animator.SetFloat("MoveY", moveY);
                UpdateSprite(); 
            
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if(moveX == 0.1)
                    animator.SetTrigger("Attack");
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
            
            isCanMove = playerInformationPanel.activeSelf? false : true;
        }
    }
    private void UpdateSprite()
    {
        if (moveY > 0) // Hướng lên
        {
            spriteRenderer.sprite = spriteDirection[0];
            direction = "Up";
        }
        else if (moveY < 0) // Hướng xuống
        {
            spriteRenderer.sprite = spriteDirection[1];
            direction = "Down";
        }
        else if (moveX < 0) // Hướng trái
        {
            spriteRenderer.sprite = spriteDirection[2];
            direction = "Left";
        }
        else if (moveX > 0) // Hướng phải
        {
            spriteRenderer.sprite = spriteDirection[3];
            direction = "Right";
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
                enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(playerStats.damage);
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
            var oneItemButton = itemButton[i];
            oneItemButton.transform.GetChild(0).gameObject.SetActive(true);
            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = items[i].item.itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = items[i].quantity.ToString();
        }

    }
}
