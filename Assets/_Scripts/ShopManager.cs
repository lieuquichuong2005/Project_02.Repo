using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventoryManager playerInventoryManager; 

    public List<Item> itemsInShop;
    public Button[] itemsShopButton;

    public GameObject itemInformationPanel;

    public Button closeButton;
    public Button buyButton;

    public TMP_InputField amountInputField;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemTypeText; 
    public TMP_Text itemPriceText;
    public TMP_Text itemDescriptionText;
    public TMP_Text coinText;

    private Item selectedItem; 

    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

        for (int i = 0; i < itemsInShop.Count; i++)
        {
            var oneItemButton = itemsShopButton[i];
            int index = i;

            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = itemsInShop[i].itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = itemsInShop[i].itemName;

            oneItemButton.onClick.AddListener(() => OnItemButtonClick(itemsInShop[index]));
        }
        coinText.text = "Coin: " + PlayerStats.coin.ToString();
        itemInformationPanel.SetActive(false);
    }

    void OnItemButtonClick(Item clickedItem)
    {
        if (clickedItem == null)
        {
            Debug.LogError("Item Trống");
            return;
        }

        selectedItem = clickedItem;
        itemImage.sprite = clickedItem.itemSprite;
        itemNameText.text = clickedItem.itemName;
        itemTypeText.text = clickedItem.itemType; 
        itemPriceText.text = "Giá: " + clickedItem.itemPrice.ToString();
        itemDescriptionText.text = clickedItem.itemDescription;

        amountInputField.text = "1"; 
        itemInformationPanel.SetActive(true);
    }

    void OnBuyButtonClick()
    {
        if (selectedItem == null)
        {
            Debug.LogError("Chưa chọn món đồ để mua");
            return;
        }

        if (int.TryParse(amountInputField.text, out int amountItemBuy) && amountItemBuy > 0)
        {
            int totalPrice = selectedItem.itemPrice * amountItemBuy;
            if (PlayerStats.coin > totalPrice)
            {
                playerInventoryManager.AddItem(selectedItem);
                playerStats.UseCoin(20);
                Debug.Log($"Mua {amountItemBuy} sản phẩm: {selectedItem.itemName} với tổng giá {totalPrice}");
            }
            else
                Debug.Log("Tài sản hiện có không đủ");
        }
        else
        {
            Debug.LogError("Số lượng không hợp lệ");
        }
    }

    void OnCloseButtonClick()
    {
        this.gameObject.SetActive(false);
    }
}