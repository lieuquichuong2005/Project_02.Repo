[System.Serializable]
public class PlayerData
{
    public string id;
    public string name;
    public int level;
    public int exp;
    public int gold;
    public string[] items;

    public PlayerData(string id, string name, int level, int exp, int gold, string[] items)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.exp = exp;
        this.gold = gold;
        this.items = items;
    }
}
