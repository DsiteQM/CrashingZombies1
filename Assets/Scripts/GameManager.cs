using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameObject shopUI;
    public GameObject garageDoor;

    public int zombiesToKill = 10;
    public TextMeshProUGUI zombiesToKillText;
    private GameObject zombieSpawner;
    ExperienceSystem experienceSystem;

    private void Awake()
    {
        experienceSystem = GetComponent<ExperienceSystem>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        zombieSpawner = GameObject.FindGameObjectWithTag("ZombieSpawner");
        
    }

    void Update()
    {
        UpdateZombieCount();
        if (zombiesToKill<= 0)
        {
            zombiesToKill = 0;
            ClearZombies();

        }
    }

    public void ClearZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            Destroy(zombie);
        }
         zombieSpawner.SetActive(false);
    }

    public void NextWave()
    {
        zombieSpawner.SetActive(true);
        zombieSpawner.GetComponent<ZombieSpawner>().DecreaseZombieSpawnTime();
        zombiesToKill += 50;
        experienceSystem.UpdateExp();
        
    }

    void UpdateZombieCount()
    {
        zombiesToKillText.text = zombiesToKill.ToString();  
    }
    private void OpenGarage()
    {
        Vector3 upForce = new Vector3(garageDoor.transform.position.x, 20f, garageDoor.transform.position.z);
        garageDoor.transform.position =Vector3.Lerp( garageDoor.transform.position, upForce, Time.deltaTime*10f); 
    }

    public void CloseGarage()
    {
        Vector3 downForce = new Vector3(garageDoor.transform.position.x, 6f, garageDoor.transform.position.z);
        garageDoor.transform.position = Vector3.Lerp(downForce, garageDoor.transform.position,  Time.deltaTime * 20f);
        NextWave();
    }
}
