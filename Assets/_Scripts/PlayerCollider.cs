using NUnit.Framework;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventoryManager inventoryManager;
    public PlayerMovement playerMovement;
    public ShopManager shopManager;
    public NotificationManager notificationManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            var item = other.gameObject.GetComponent<Item>();
            inventoryManager.AddItem(item);
            notificationManager.ShowNotification(item.itemSprite, item.itemName);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Monster"))
        {
            playerStats.currentHealth -= 2; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            playerStats.currentHealth -= 10; 
        }
        else if (collision.gameObject.CompareTag("BlacksmithNPC"))
        {
            playerMovement.isNearToBlacksmithNPC = true;
        }
        else if (collision.gameObject.CompareTag("HerbalistNPC"))
        {
            playerMovement.isNearToHerbalistNPC = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlacksmithNPC"))
        {
            playerMovement.isNearToBlacksmithNPC = false;
        }
        if (collision.gameObject.CompareTag("HerbalistNPC"))
        {
            playerMovement.isNearToHerbalistNPC = false;
        }
    }

}