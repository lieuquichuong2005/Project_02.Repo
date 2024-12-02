using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCollider : MonoBehaviour
{
    public List<ItemInventory> itemInventory; 
    public PlayerStats playerStats;

    private void Start()
    {
        itemInventory = new List<ItemInventory>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            var item = other.gameObject.GetComponent<Item>();
            var checkItem = itemInventory.Find(x => x.item.itemID == item.itemID);

            if (checkItem == null)
            {
                itemInventory.Add(new ItemInventory { item = item, quantity = 1 });
            }
            else
                checkItem.quantity++;
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
    }
}
