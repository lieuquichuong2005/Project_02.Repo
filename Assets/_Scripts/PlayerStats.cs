using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    public Image expMount;
    public TMP_Text levelText;

    private float maxHealth = 100;
    public float currentHealth;
    private float maxMana = 100;
    public float currentMana;
    private float maxExperience = 100;
    public float currentExperience;
    
    public float moveSpeed;
    private float level = 1;

    private void Awake()
    {
        healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.FindWithTag("ManaSlider").GetComponent <Slider>();
        expMount = GameObject.FindWithTag("ExpMount").GetComponent<Image>();
        levelText = GameObject.FindWithTag("LevelText").GetComponent <TMP_Text>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentExperience = 0;
        moveSpeed = 3f;

        healthSlider.value = currentHealth / maxHealth;
        manaSlider.value = currentMana / maxMana;
        levelText.text = level.ToString();
        expMount.fillAmount = currentExperience/maxExperience;
    }
}
