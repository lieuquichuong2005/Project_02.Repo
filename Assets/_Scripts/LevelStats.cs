[System.Serializable]
public class LevelStats
{
    public int level;
    public int maxHealth;
    public int maxMana;
    public int damage;
    public int armor;
    public int moveSpeed;
    public int attackSpeed;

    public LevelStats(int level, int maxHealth, int maxMana, int damage, int armor, int moveSpeed, int attackSpeed)
    {
        this.level = level;
        this.maxHealth = maxHealth;
        this.maxMana = maxMana;
        this.damage = damage;
        this.armor = armor;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
    }
}