using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventoryManager playerInventoryManager;

    public List<Item> armorItemsInShop;
    public List<Item> consumeItemsOutShop; 
    public List<Item> weaponItemsOutShop;
    public GameObject itemButtonPrefab;
    public Transform contentPanel;

    public GameObject[] shopPanels;
    public GameObject itemInformationPanel;

    public Button closeButton;
    public Button buyButton;

    public TMP_Text shopNameText;
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

        PopulateShop(armorItemsInShop); 
        UpdateCoinText();
        itemInformationPanel.SetActive(false);
    }

    public void PopulateShop(List<Item> shopItems)
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in shopItems)
        {
            GameObject oneItemButton = Instantiate(itemButtonPrefab, contentPanel);
            oneItemButton.transform.GetChild(0).GetComponent<Image>().sprite = item.itemSprite;
            oneItemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemName;

            oneItemButton.GetComponent<Button>().onClick.AddListener(() => OnItemButtonClick(item));
        }
    }

    void OnItemButtonClick(Item clickedItem)
    {
        if (clickedItem == null)
        {
            Debug.LogError("Item trống");
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
            if (PlayerStats.coin >= totalPrice)
            {
                playerInventoryManager.AddItem(selectedItem);
                playerStats.UseCoin(totalPrice);
                Debug.Log($"Mua {amountItemBuy} sản phẩm: {selectedItem.itemName} với tổng giá {totalPrice}");
                UpdateCoinText();
            }
            else
            {
                Debug.Log("Tài sản hiện có không đủ");
            }
        }
        else
        {
            Debug.LogError("Số lượng không hợp lệ");
        }
    }

    void OnCloseButtonClick()
    {
        itemInformationPanel.SetActive(false);
        this.gameObject.SetActive(false); 
    }

    void UpdateCoinText()
    {
        coinText.text = "Coin: " + PlayerStats.coin.ToString(); 
    }

    public void PanelToActive(GameObject panelToActive)
    {
        foreach (GameObject panel in shopPanels)
        {
            panel.SetActive(panelToActive == panel);
        }
    }

    public void SwitchShop(string shopType)
    {
        switch (shopType)
        {
            case "Armor":
                shopNameText.text = "Armor Shop"; 
                PopulateShop(armorItemsInShop);
                break;
            case "Consume":
                shopNameText.text = "Herbal Shop";
                PopulateShop(consumeItemsOutShop);
                break;
            case "Weapon":
                shopNameText.text = "Weapon Shop";
                PopulateShop(weaponItemsOutShop);
                break;
            default:
                Debug.LogError("Loại shop không hợp lệ");
                break;
        }
    }
}