using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    PlayerStats playerStats;
    InventoryUI inventoryUI;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        inventoryUI = GetComponent<InventoryUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            if (other.CompareTag("Item"))
            {
                //Item item = other.GetComponent<ItemPickup>().item; // Giả sử bạn có một script ItemPickup
                //inventoryUI.AddItemToInventory(item);
                Destroy(other.gameObject); // Xóa vật phẩm khỏi game
            }
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
