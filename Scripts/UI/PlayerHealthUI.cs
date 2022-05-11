using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image healthSlider;
    public Image strengthSlider;
    private CharacterStats playerCharacterStats;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacterStats=GameManager.Instance.Player.GetComponent<CharacterStats>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateStrength();   
    }
    void UpdateHealth()
    {
        float sliderPercent = (float)playerCharacterStats.CurrentHealth / playerCharacterStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }
    void UpdateStrength()
    {
        float sliderPercent = (float)playerCharacterStats.CurrentPower / 100;
        strengthSlider.fillAmount = sliderPercent;
    }
}
