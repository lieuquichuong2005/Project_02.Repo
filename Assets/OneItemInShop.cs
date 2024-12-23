using UnityEngine;

public class OneItemInShop : MonoBehaviour
{
    public ShopManager shopManager;
    public Item itemInShop;
    
    public void SetItem()
    {
            shopManager.selectItem = itemInShop;
    }    
}
