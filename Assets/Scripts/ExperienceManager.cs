using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; set; }

    public delegate void ExperienceChangeHandler(int amount);

    public event ExperienceChangeHandler OnExperienceChanged;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
        else 
        { 
            Destroy(gameObject);
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChanged?.Invoke(amount);
    }

}
