using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCollider : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventoryManager inventoryManager;
    public PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            var item = other.gameObject.GetComponent<Item>();
            inventoryManager.AddItem(item);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Monster"))
        {
            playerStats.currentHealth -= 2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            playerStats.currentHealth -= 10;
        }
        else if (collision.gameObject.CompareTag("BlacksmithNPC"))
        {
            if (playerMovement.shopPanel != null)
                playerMovement.shopPanel.gameObject.SetActive(true);
            else Debug.LogWarning("Lỗi");
        }
    }
}
