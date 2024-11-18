using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider expSlider;
    public TMP_Text levelText;

    private float currentHealth;
    private float currentMana;
    private float maxHealth = 100;
    private float maxMana = 100;
    
    public float moveSpeed;
    private float level = 1;

    private void Awake()
    {
        healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.FindWithTag("ManaSlider").GetComponent <Slider>();
        expSlider = GameObject.FindWithTag("ExpSlider").GetComponent<Slider>();
        levelText = GameObject.FindWithTag("LevelText").GetComponent <TMP_Text>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        moveSpeed = 3f;

        healthSlider.value = currentHealth / maxHealth;
        manaSlider.value = currentMana / maxMana;
        levelText.text = level.ToString();

    }
}
