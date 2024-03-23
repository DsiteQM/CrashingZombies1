using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public int currentExperience, maxExperience, currentLevel;
    public Image expBarImage;
    public TextMeshProUGUI expBarText;

    public GameObject upgrades;


    private void Start()
    {
        UpdateExp();
        currentExperience = 0;
        expBarImage.fillAmount = 0;
        UpdateExpBar();
        ExperienceManager.Instance.OnExperienceChanged += HandleExperienceChange;
    }
    private void Update()
    {   
        UpdateExpBar();
        SaveExp();
    }

    private void OnEnable()
    {
        //ExperienceManager.Instance.OnExperienceChanged += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChanged -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;

        if(currentExperience > maxExperience) 
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        upgrades.SetActive(true);
        Time.timeScale = 0;

        currentLevel++;

        currentExperience= 0;
        maxExperience *= 2;
    }

    private void UpdateExpBar()
    {
        expBarImage.fillAmount = GetExpNormalized();
        expBarText.text = "Level "+ currentLevel.ToString()+ " :  "+  currentExperience.ToString() + " / " + maxExperience.ToString();
    }
    public float GetExpNormalized()
    {
        return (float)currentExperience / maxExperience;
    }

    public void UpdateExp()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentExperience = PlayerPrefs.GetInt("currentExperience");
        maxExperience = PlayerPrefs.GetInt("maxExperience");
    }
    public void SaveExp()
    {
        PlayerPrefs.SetInt("currentExperience", currentExperience);
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetInt("maxExperience" , maxExperience);
    }

    public void ResetExp()
    {
        PlayerPrefs.SetInt("currentExperience", 0);
        PlayerPrefs.SetInt("currentLevel", 1);
        PlayerPrefs.SetInt("maxExperience", 100);
    } 
}
