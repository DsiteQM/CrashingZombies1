using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public HealthSystem healthSystem;
    public GameObject turretPrefab;
    public GameObject carUpgrade1;
    public TextMeshProUGUI notEnoughCoinsText;
   
    public void BuyTurret()
    {
        turretPrefab.SetActive(true);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BuyHealth()
    {
        int healAmount = 10;
        healthSystem.IncreaseCurrentHealth(healAmount);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpgradeCar()
    {
        int additionalHealth = 100;        

            healthSystem.IncreaseMaxHealth(additionalHealth);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseTab()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
