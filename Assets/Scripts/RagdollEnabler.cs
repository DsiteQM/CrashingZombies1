using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{

    public Animator animator;
    public Transform ragdollRoot;

    public bool startRagdoll = false;

    public Rigidbody[] rigidbodies;
    public CharacterJoint[] joints;
    private Collider[] colliders;

    private void Awake()
    {
        rigidbodies= ragdollRoot.GetComponentsInChildren<Rigidbody>();
        joints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        colliders = ragdollRoot.GetComponentsInChildren<Collider>();


        if (startRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;

        foreach(CharacterJoint joint in joints)
        {
            joint.enableCollision = true;
        }

        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.velocity= Vector3.zero;
            rigidbody.detectCollisions= true;
            rigidbody.useGravity= true;
        }
    }

    public void EnableAnimator()
    {
        animator.enabled = true;

        foreach (CharacterJoint joint in joints)
        {
            joint.enableCollision = false;
        }

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
        }
    }
}
