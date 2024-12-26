using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;
    public string itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemPrice;
    public int levelToUse;
    public override string ToString()
    {
        return $"id: {itemID} name: {itemName}";
    }
}
