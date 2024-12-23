using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public GameObject itemInformationPanel;

    public Button[] itemsShopButton;
    public Button closeButton;
    public Button buyButton;

    public TMP_InputField amountInputField;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemTypeText;
    public TMP_Text itemPriceText;
    public TMP_Text itemDescriptionText;

    private int amountItemBuy;
    public Item selectItem;
    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

        foreach (Button itemButton in itemsShopButton)
        {
            itemButton.onClick.AddListener(() => OnItemButtonClick(itemButton));
            Debug.Log("Ấn Nút");
        }


        // Ẩn panel thông tin sản phẩm ban đầu
        itemInformationPanel.SetActive(false);
    }

    void OnItemButtonClick(Button clickedButton)
    {
        Debug.Log("Hiện Thông Tin");
        itemImage.sprite = clickedButton.GetComponent<Item>()?.itemSprite;
        itemNameText.text = clickedButton.GetComponent<Item>()?.itemName;
        //itemTypeText.text = clickedButton.GetComponent<Item>()?.itemType;
        itemPriceText.text = clickedButton.GetComponent<Item>()?.itemPrice.ToString();
        itemDescriptionText.text = clickedButton.GetComponent<Item>()?.itemDescription.ToString();
        amountInputField.text = "1";

        itemInformationPanel.SetActive(true);
    }

    void OnBuyButtonClick()
    {
        if (amountInputField != null) amountInputField.text = "1";
        amountItemBuy = int.Parse(amountInputField.text);
        Debug.Log($"Mua {amountInputField.text} sản phẩm: {itemNameText} số lượng {amountItemBuy} với giá { int.Parse(itemPriceText.text) * amountItemBuy}" );
    }

    void OnCloseButtonClick()
    {
        this.gameObject.SetActive(false);
    }    
}
