using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;

    public override string ToString()
    {
        return $"id: {itemID} name: {itemName}";
    }
}
