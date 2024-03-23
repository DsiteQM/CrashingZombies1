using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float bulletSpeed= 15f;
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            float distanceThisFrame = bulletSpeed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                return;
            }
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
    }

    public void Target(Transform _target)
    {
        target = _target;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
