using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class ItemInventory 
{
    public Item item {  get; set; }
    public int quantity { get; set; }
    public ItemInventory(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

}
