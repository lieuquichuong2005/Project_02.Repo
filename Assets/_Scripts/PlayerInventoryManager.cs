using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCollider playerCollider;
    public PlayerStats playerStats;
    public ItemSlotUI itemSlotUI;

    public List<ItemInventory> itemInventory;
    [SerializeField] GameObject[] itemButton;
    public GameObject informationPanel;
    public Button useItemButton;
    public Button upgradeItemButton;
    public Button throwItemButton;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Image itemImage;

    private void Awake()
    {
        useItemButton.onClick.AddListener(OnUseItemButton);
        upgradeItemButton.onClick.AddListener(OnUpgradeItemButton);
        throwItemButton.onClick.AddListener(OnThrowItemButton);
    }
    public void AddItem(Item item)
    {
        
        var checkItem = itemInventory.Find(x => x.item.itemID == item.itemID);

        if (checkItem == null)
        {
            itemInventory.Add(new ItemInventory (item, 1 ));
        }
        else
            checkItem.quantity++;
    }

    public void ShowItemInInventory()
    {
        informationPanel.gameObject.SetActive(false);

        for (int i = 0; i < itemButton.Length; i++)
        {
            var oneItemButton = itemButton[i];
            var button = oneItemButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners(); 
            oneItemButton.transform.GetChild(0).gameObject.SetActive(false);
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = " ";
        }

        for (int i = 0; i < itemInventory.Count; i++)
        {
            var oneItemButton = itemButton[i];
            var button = oneItemButton.GetComponent<Button>();
            var currentItem = itemInventory[i];

            itemSlotUI = oneItemButton.GetComponent<ItemSlotUI>();
            itemSlotUI.SetItem(currentItem.item);

            int temp = i;

            button.onClick.AddListener(() => ShowItemDetails(temp));

            oneItemButton.transform.GetChild(0).gameObject.SetActive(true);
            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.item.itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = currentItem.quantity.ToString();
        }
    }


    public void UseItem(Item item)
    {
        if (item is ConsumeItems consumable)
        {
            playerStats.currentHealth += consumable.healthAdded;
            playerStats.currentMana += consumable.manaAdded;
            playerStats.moveSpeed += consumable.moveSpeedAdded;

            var inventoryEntry = itemInventory.Find(x => x.item.itemID == consumable.itemID);
            if (inventoryEntry != null)
            {
                inventoryEntry.quantity--;
                if (inventoryEntry.quantity <= 0)
                {
                    itemInventory.Remove(inventoryEntry);
                }

                Debug.Log($"Used item: {item.itemName}, Remaining: {inventoryEntry.quantity}");
                ShowItemInInventory();
            }
        }
    }

    public void ShowItemDetails(int  index)
    {

        if (itemInventory.Count <= index) return;
        var item = itemInventory[index].item;
        if (item.itemName != null)
        {
            itemNameText.text = item.itemName; 
            itemDescriptionText.text = item.itemDescription;
            itemImage.sprite = item.itemSprite;
            Debug.Log($"Clicked on item: {item.itemName}, Description: {item.itemDescription}");
            informationPanel.gameObject.SetActive(true); 
        }
        else
        {
            itemNameText.text = "No Item";
            itemDescriptionText.text = "This slot is empty.";
            Debug.Log("No item found in this slot.");
        }
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

}
