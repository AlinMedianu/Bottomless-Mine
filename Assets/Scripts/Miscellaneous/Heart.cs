using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Heart : MonoBehaviour
{
    [SerializeField]
    private float speed;            //the speed at which the heart is moving towards the negative z axis
    private Rigidbody rb;

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    private void Update ()
    {
        rb.velocity = Vector3.back * speed * Time.deltaTime;
        if (rb.position.z < -6.5f)
            Destroy(gameObject);
    }
}
