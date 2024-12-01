using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCollider playerCollider;
    public PlayerStats playerStats;

    [SerializeField] GameObject[] itemButton;
    public GameObject informationPanel;
    public Button useItemButton;
    public Button upgradeItemButton;
    public Button throwItemButton;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    //public Item hoveredItem;

    private void Awake()
    {
        useItemButton.onClick.AddListener(OnUseItemButton);
        upgradeItemButton.onClick.AddListener(OnUpgradeItemButton);
        throwItemButton.onClick.AddListener(OnThrowItemButton);
    }


    void OnUseItemButton()
    {
        ShowItemInInventory();
    }
    public void OnUpgradeItemButton()
    {

    }
    void OnThrowItemButton()
    {
        ShowItemInInventory();
    }

    public void ShowItemInInventory()
    {
        informationPanel.gameObject.SetActive(false);
        // Ẩn tất cả các nút item trước
        for (int i = 0; i < itemButton.Length; i++)
        {
            var oneItemButton = itemButton[i];
            oneItemButton.transform.GetChild(0).gameObject.SetActive(false);
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = " ";
        }

        // Hiển thị các item trong inventory
        var items = playerCollider.itemInventory;
        int maxItemsToShow = Mathf.Min(itemButton.Length, items.Count); // Giới hạn số lượng hiển thị

        for (int i = 0; i < maxItemsToShow; i++)
        {
            var oneItemButton = itemButton[i];
            oneItemButton.transform.GetChild(0).gameObject.SetActive(true);
            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = items[i].item.itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = items[i].quantity.ToString();
        }
    }


    public void UseItem(Item item)
    {
        if (item is ConsumeItems consumable)
        {
            Debug.Log("Dùng Vật Phẩm");
            // Áp dụng hiệu ứng từ vật phẩm
            playerStats.currentHealth += consumable.healthAdded;
            playerStats.currentMana += consumable.manaAdded;
            playerStats.moveSpeed += consumable.moveSpeedAdded;

            // Tìm vật phẩm trong inventory và giảm số lượng
            var inventoryEntry = playerCollider.itemInventory.Find(x => x.item.itemID == consumable.itemID);
            if (inventoryEntry != null)
            {
                inventoryEntry.quantity--;
                if (inventoryEntry.quantity <= 0)
                {
                    playerCollider.itemInventory.Remove(inventoryEntry); // Xóa item nếu số lượng bằng 0
                    //hoveredItem = null; // Xóa hoveredItem nếu vật phẩm bị xóa
                }

                Debug.Log($"Used item: {item.itemName}, Remaining: {inventoryEntry.quantity}");
                ShowItemInInventory(); // Cập nhật lại giao diện inventory
            }
        }
    }


    public void SetHoveredItem(Item item)
    {
        //hoveredItem = item;
        Debug.Log($"Hovered Item: {item.itemName}");
    }


    public void ClearHoveredItem()
    {

        //hoveredItem = null; // Xóa item đang trỏ vào
        Debug.Log("No item hovered");
    }
    public void OnItemButtonClick()
    {
        
        informationPanel.gameObject.SetActive(true);
    }
}
