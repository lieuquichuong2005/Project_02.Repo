using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public Image playerImage;
    public Image weaponImage;
    public Image shieldImage;
    public Image helmetImage;
    public Image armorImage;
    public Image pantImage;
    public Image shoeImage;

    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text levelText;
    public TMP_Text damageText;
    public TMP_Text armorText;
    public TMP_Text expText;
    public TMP_Text moveSpeedText;
    public TMP_Text attackSpeedText;
    public TMP_Text characterTypeText;

    private void Start()
    {
        playerImage.sprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().sprite;

        healthText.text = "Health: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
        manaText.text = "Mana: " + playerStats.currentMana + "/" + playerStats.maxMana;
        levelText.text = "Level: " + playerStats.level.ToString();
        damageText.text = "Damage: " + playerStats.damage.ToString();
        armorText.text = "Armor: " + playerStats.armor.ToString();
        expText.text = "Exp: " + playerStats.currentExperience + "/" + playerStats.experienceToLevelUp;
        moveSpeedText.text = "MoveSpeed: " + playerStats.moveSpeed.ToString();
        attackSpeedText.text = "AttackSpeed: " + playerStats.attackSpeed.ToString();
        characterTypeText.text = "None";
    }
}
