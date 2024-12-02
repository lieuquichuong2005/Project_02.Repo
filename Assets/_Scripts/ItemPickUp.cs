using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemSlotUI itemSlotUI;
    public Item item;
    
    void PickUp()
    {
        itemSlotUI.SetItem(item);
        Destroy(this.gameObject);
    }
}
