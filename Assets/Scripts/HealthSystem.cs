using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarImage;
    public TextMeshProUGUI HealthBarText;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarImage.fillAmount = 1;
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = GetHealthNormalized();
        HealthBarText.text = currentHealth.ToString() + " / " +maxHealth.ToString();
    }

    public float GetHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }

    public void IncreaseCurrentHealth(int _currentHealth)
    {
        currentHealth+= _currentHealth;
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

    }

    public void IncreaseMaxHealth(int _maxHealth)
    {
        maxHealth += _maxHealth;
    }

    public void Die()
    {
        
    }
}
