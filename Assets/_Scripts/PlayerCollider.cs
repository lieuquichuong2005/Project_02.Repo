using NUnit.Framework;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventoryManager inventoryManager;
    public PlayerMovement playerMovement;
    public ShopManager shopManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            var item = other.gameObject.GetComponent<Item>();
            inventoryManager.AddItem(item);
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
            OpenShop("Armor"); 
        }
        else if (collision.gameObject.CompareTag("HerbalistNPC"))
        {
            OpenShop("Consume");
        }
        /*else if (collision.gameObject.CompareTag("WeaponNPC"))
        {
            OpenShop("Weapon"); 
        }*/
    }

    private void OpenShop(string shopType)
    {
        playerMovement.shopPanel.SetActive(true);
        shopManager.SwitchShop(shopType); 
    }
}