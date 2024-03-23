using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float lerping;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position+ offset, Time.deltaTime* lerping );

        transform.LookAt(target);
    }


}
