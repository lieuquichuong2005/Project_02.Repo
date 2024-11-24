using UnityEngine;

public class Item
{
    public string itemName;
    public string description;
    public Sprite icon; // Hình ảnh đại diện cho vật phẩm
    public int quantity;

    public Item(string name, string desc, Sprite icon, int qty = 1)
    {
        itemName = name;
        description = desc;
        this.icon = icon;
        quantity = qty;
    }
}