using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab; // Prefab cho ô vật phẩm
    private Inventory inventory;

    void Start()
    {
        inventory = new Inventory();
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        // Xóa các ô cũ
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.GetItems())
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel.transform);
            slot.GetComponentInChildren<Image>().sprite = item.icon;
            slot.GetComponentInChildren<Text>().text = item.quantity.ToString();
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (inventory.AddItem(item))
        {
            UpdateInventoryUI();
        }
    }
}