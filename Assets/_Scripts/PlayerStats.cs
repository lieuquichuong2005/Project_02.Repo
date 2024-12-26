using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ShopManager shopManager;

    public List<LevelStats> levelStats;

    public Slider healthSlider;
    public Slider manaSlider;
    public Image expMount;
    public TMP_Text levelText;
    public TMP_Text coinText;
 
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
    public static int coin = 0;

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
            new LevelStats(11, 1300, 500, 300, 70, 6, 4),
            new LevelStats(12, 1300, 500, 300, 70, 6, 4),
            new LevelStats(13, 1300, 500, 300, 70, 6, 4),
            new LevelStats(14, 1300, 500, 300, 70, 6, 4),
            new LevelStats(15, 1300, 500, 300, 70, 6, 4),
            new LevelStats(16, 1300, 500, 300, 70, 6, 4),
            new LevelStats(17, 1300, 500, 300, 70, 6, 4),
            new LevelStats(18, 1300, 500, 300, 70, 6, 4),
            new LevelStats(19, 1300, 500, 300, 70, 6, 4),
            new LevelStats(20, 1300, 500, 300, 70, 6, 4),
            new LevelStats(21, 1300, 500, 300, 70, 6, 4),
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
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
        levelText.text = level.ToString();
        expMount.fillAmount = (float)currentExperience / experienceToLevelUp;
        coinText.text = coin.ToString();

        float expPercentage = (float)currentExperience/ experienceToLevelUp;
        expMount.GetComponent<Image>().color = Color.Lerp(Color.yellow, Color.green, expPercentage);
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
            StartCoroutine(playerMovement.FlashMarkerColor());
        }
    }

    private int CalculateExperienceToLevelUp(int currentLevel)
    {
        return 215 * currentLevel;
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

    public void GainCoin(int amount)
    {
        coin += amount;
        shopManager.coinText.text = coin.ToString();
        UpdateStatsUI();
    }
    public void UseCoin(int amount)
    {
        coin -= amount;
        shopManager.coinText.text = coin.ToString();
        UpdateStatsUI();
    }
        
    private void ShowLevelUpEffect()
    {
        
        Invoke("HideLevelUpText", 2f); 
    }
    
}
