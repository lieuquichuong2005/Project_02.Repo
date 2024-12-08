using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public List<LevelStats> levelStats;

    public Slider healthSlider;
    public Slider manaSlider;
    public Image expMount;
    public TMP_Text levelText;
 
    public int level = 1;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int experienceToLevelUp;
    public int currentExperience;
    public int damage;
    public int armor;
    public int attackSpeed;
    public int moveSpeed;

    private void Awake()
    {
        levelStats = new List<LevelStats>
        {
            new LevelStats(1, 100, 50, 10, 10, 3, 2),
            new LevelStats(2, 150, 70, 20, 13, 3, 2),
            new LevelStats(3, 220, 90, 45, 15, 4, 3),
            new LevelStats(4, 300, 120, 50, 18, 4, 3),
            new LevelStats(5, 400, 150, 70, 23, 5, 3),
            new LevelStats(6, 520, 200, 100, 29, 5, 3),
            new LevelStats(7, 680, 250, 130, 35, 5, 3),
            new LevelStats(8, 850, 310, 170, 45, 6, 4),
            new LevelStats(9, 1050, 390, 220, 59, 6, 4),
            new LevelStats(10, 1300, 500, 300, 70, 6, 4),
        };
        if (level < 1 || level > levelStats.Count)
        {
            level = 1; 
        }
        UpdateStatsData(levelStats[level - 1]);
        UpdateStatsUI();
    }
    public void UpdateStatsData(LevelStats stats)
    {
        level = stats.level;
        maxHealth = stats.maxHealth;
        currentHealth = maxHealth;
        maxMana = stats.maxMana;
        currentMana = maxMana;
        damage = stats.damage;
        armor = stats.armor;
        moveSpeed = stats.moveSpeed;
        attackSpeed = stats.attackSpeed;
        experienceToLevelUp = CalculateExperienceToLevelUp(level);
    }
    public void UpdateStatsUI()
    {
        healthSlider.value = currentHealth/maxHealth;
        manaSlider.value = currentMana/maxMana;
        levelText.text = level.ToString();
        expMount.fillAmount = (float)currentExperience / (float)experienceToLevelUp;
    }
    public void GainExperience(int amount)
    {
        currentExperience += amount;
        UpdateStatsUI();
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentExperience >= experienceToLevelUp)
        {
            currentExperience -= experienceToLevelUp;
            level++;
            experienceToLevelUp = CalculateExperienceToLevelUp(level);
            IncreaseStats();
        }
    }

    private int CalculateExperienceToLevelUp(int currentLevel)
    {
        return 95 * currentLevel;
    }

    private void IncreaseStats()
    {
        if (level - 1 < levelStats.Count)
        {
            LevelStats stats = levelStats[level - 1];
            UpdateStatsData(stats);
            UpdateStatsUI();
            Debug.Log($"Level Up! Level: {level}, Health: {maxHealth}, Mana: {maxMana}");
        }
    }
    public void UpdateStatsByUsingConsumeItem(int hp, int mp, int speed)
    {
        this.currentHealth += hp;
        this.currentMana += mp;
        this.currentExperience += speed;
    }
    public void EarnDamage(int damage)
    {
        this.currentHealth -= damage;
        Debug.Log($"Máu hiện tại: {currentHealth}");
        UpdateStatsUI();
    }
    
    public int GetLevel()
    {
        return level;
    }
}
