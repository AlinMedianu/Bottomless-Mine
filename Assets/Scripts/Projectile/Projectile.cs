using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float selfDestructionTimer;
    [SerializeField]
    protected string[] ignoreTags;
    protected Rigidbody rb;

    protected virtual void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = false;
        Destroy(gameObject, selfDestructionTimer);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
        {
            bool ignored = false;
            foreach (string tag in ignoreTags)
                if (other.tag == tag)
                    ignored = true;
            if (!ignored)
            {
                other.GetComponent<Character>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }      
    }
}
