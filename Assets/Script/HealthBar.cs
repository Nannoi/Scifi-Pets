using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _healthBarSprite;
    public TMP_Text health; 
    
    public void UpdateHealthBar(int maxHealth,int currentHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        _healthBarSprite.fillAmount = fillAmount;
        health.text = $"{currentHealth}/{maxHealth}";
    }
}
