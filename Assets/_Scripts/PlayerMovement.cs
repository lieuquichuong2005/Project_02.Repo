using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviourPun
{
    public static PlayerMovement instance;
    public static string currentScene;

    public PlayerInventoryManager inventoryManager;
    public PlayerStats playerStats;
    public ShopManager shopManager;
    public PlayerCollider playerCollider;
    public DialogueManager dialogueManager;
    public SettingInGame settingInGame;

    public GameObject playerInformationPanel;
    public GameObject settingInGamePanel;
    public GameObject marker;
    public GameObject AttackPointObj;
    public GameObject shopPanel;
    public GameObject notifyPanel;

    public TMP_Text notifyText;
    public Transform attackPoint;

    private Rigidbody2D rb2d;
    public Animator animator;
    public LayerMask enemyLayers;

    public string playerName;
    public Sprite playerAvatar;
    public bool isCanMove = true;
    public float attackRangeDefault;
    public float attackRange = 0f;
    public int damage = 20;
    private string direction;
    float moveX;
    float moveY;

    public bool isNearToBlacksmithNPC;
    public bool isNearToHerbalistNPC;

    public AudioSource sword_sound;

    void Awake()
    {
        playerName = "Player";
        /*&if (instance == null)
            instance = this;
        else
            Destroy(instance);*/
        marker.SetActive(true);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AttackPointObj.SetActive(false);

        currentScene = "Làng Tân Thủ";
        playerInformationPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (isCanMove)
            {
                moveX = Input.GetAxisRaw("Horizontal");
                moveY = Input.GetAxisRaw("Vertical");
                if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
                {
                    animator.SetFloat("LastMoveX", moveX);
                    animator.SetFloat("LastMoveY", moveY);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sword_sound.Play();
                    AttackPointObj.SetActive(true);
                    animator.SetTrigger("Attack");
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

            CheckGetKey();
  
            // Kiểm tra xem có item nào dưới con trỏ chuột không
            if (playerInformationPanel.activeSelf) // Nếu panel đang mở
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    var item = hit.collider.GetComponent<Item>();
                    if (item != null)
                    {
                        //inventoryManager.hoveredItem = item; // Ghi nhận item đang trỏ vào
                    }
                }
                else
                {
                    //inventoryManager.hoveredItem = null; // Không trỏ vào item nào
                }

                /*if (Input.GetKeyDown(KeyCode.E) && inventoryManager.hoveredItem != null)
                {
                    inventoryManager.UseItem(inventoryManager.hoveredItem); 
                   
                }*/

            }

            isCanMove = (playerInformationPanel.activeSelf || settingInGamePanel.activeSelf || shopPanel.activeSelf || dialogueManager.dialogueSystem.activeSelf) ? false : true;

            
        }
    }

    void CheckGetKey()
    {
        if(isNearToBlacksmithNPC && Input.GetKeyDown(KeyCode.Space))
        {
            CommunicateWithNPC();
            //OpenShop("Armor");
        }
        if(isNearToHerbalistNPC && Input.GetKeyDown(KeyCode.Space))
        {
            CommunicateWithNPC();
            //OpenShop("Consume");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            settingInGamePanel.SetActive(!settingInGamePanel.activeSelf);
            if (settingInGamePanel.activeSelf)
                settingInGame.InactivePanel();
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerInformationPanel.SetActive(!playerInformationPanel.activeSelf);
            inventoryManager.ShowItemInInventory();

        }
    }

    public void Attack()
    {
        attackRange = attackRangeDefault;
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitenemies)
        {
            if (enemy.tag == "enemy")
            {
                Debug.Log("Hit");
                enemy.gameObject.GetComponent<MonsterInteract>().ReceiveDamage(playerStats.damage);
                if (enemy.gameObject.GetComponent<MonsterInteract>().GetHealth() <= 0)
                {
                    int exp = enemy.gameObject.GetComponent<MonsterInteract>().GetEXP();
                    playerStats.GainExperience(exp);
                }
            }
        }

        attackRange = 0f;
        AttackPointObj.SetActive(false);
    }
    public IEnumerator FlashMarkerColor()
    {
        float duration = 10f;
        float interval = 0.2f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            marker.GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(interval);
            marker.GetComponent<Renderer>().material.color = Color.blue; 
            yield return new WaitForSeconds(interval);
            elapsed += interval * 2; 
        }

        marker.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeDefault);
    }
    public void OpenShop(string shopType)
    {
        shopPanel.SetActive(true);
        shopManager.SwitchShop(shopType);
    }
    public void Notity(string textNotify)
    {
        notifyPanel.gameObject.SetActive(true);
        notifyText.text = notifyText.ToString();
        StartCoroutine(WaitToTurnOffNotify(2));
    }
    IEnumerator WaitToTurnOffNotify(int time)
    {
        yield return new WaitForSeconds(time);
        notifyPanel.gameObject.SetActive(false);
    }
    public void Speak(string dialogue)
    {
        //dialogueManager.DisplayDialogue(playerName, dialogue);
    }
    public void CommunicateWithNPC()
    {
        dialogueManager.StartDialogue();
    }
        
}
