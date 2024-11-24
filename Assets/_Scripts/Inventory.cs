using System.Collections.Generic;

public class Inventory
{
    private List<Item> items = new List<Item>();
    public int maxItems = 20; // Giới hạn số vật phẩm có thể lưu

    public bool AddItem(Item item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            items.Add(item);
            return true; // Thêm thành công
        }
        return false; // Hết chỗ
    }

    public bool RemoveItem(Item item)
    {
        return items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return items;
    }
}