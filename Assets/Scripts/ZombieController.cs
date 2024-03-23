using cakeslice;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    HealthSystem healthSystem;
    RagdollEnabler ragdollEnabler;
    Outline outline;

    public GameObject bloodPrefab;
    public Transform bloodPosition;
    public GameObject target;

    private NavMeshAgent navMesh;
    public AudioSource zombieAudio;

    public float moveSpeed;
    public float attackTimer;
    public int expAmount;




    Rigidbody rb;

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        ragdollEnabler = GetComponent<RagdollEnabler>();
        outline= GetComponentInChildren<Outline>();
        navMesh = GetComponent<NavMeshAgent>();
        target = FindAnyObjectByType<CarController>().gameObject;
    }
    private void Update()
    {
        navMesh.SetDestination(target.transform.position);
        attackTimer-= Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Car" && CarController.Instance.GetSpeed() >= 10f)
        {
            Instantiate(bloodPrefab, bloodPosition);
            ExperienceManager.Instance.AddExperience(expAmount);
            GameManager.Instance.zombiesToKill--;
            ragdollEnabler.EnableRagdoll();
            outline.eraseRenderer = true;
            zombieAudio.Play();
            Destroy(gameObject, 1.5f);
            this.gameObject.tag = "Dead";
        }

        if (collision.transform.tag == "Bullet")
        {
            Instantiate(bloodPrefab, bloodPosition);
            ExperienceManager.Instance.AddExperience(expAmount);
            GameManager.Instance.zombiesToKill--;
            Destroy(gameObject, 0.3f);
        }

        if(collision.transform.tag == "Car" && CarController.Instance.GetSpeed() < 10f && attackTimer <= 0)
        {
            target.GetComponent<HealthSystem>().TakeDamage(5);
            attackTimer = 1f;
        }

    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

}
