using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Transform target;
    public Transform rotatePart;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float range;
    public float attackSpeed = 1.5f;
    private float attackCountdown;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SearchTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    
        if(target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatePart.rotation , lookRotation ,Time.deltaTime* 5f).eulerAngles;
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0);

        if (attackCountdown <= 0f && target != null)
        {
            ShootTarget();
            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;
    }

    private void ShootTarget()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet!= null)
        {
            bullet.Target(target);
        }
    }

    private void SearchTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy< shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null && shortestDistance <= range) 
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target= null;
            }
        }
    }
}
