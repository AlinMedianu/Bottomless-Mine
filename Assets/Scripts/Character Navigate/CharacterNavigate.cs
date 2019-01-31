using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the class that defines all movement behaviours (the rotation of the boss doesn't count as movement)
public abstract class CharacterNavigate : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    protected Rigidbody rb;

    public bool Moving { get; set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();         //the Rigidbody component will always be there because of the Character script
    }

    protected abstract void FixedUpdate();
}
