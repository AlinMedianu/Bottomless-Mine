using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyCharge : EnemyMove
{
    [SerializeField]
    private float boundary;
    private Vector3 currentPosition;
    private Vector3 destination;
    private Vector3 direction;

	protected override void Awake ()
    {
        base.Awake();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        currentPosition = rb.position;
        destination = new Vector3(rb.position.x, rb.position.y, -4f);
        direction = (destination - currentPosition).normalized;
    }

    protected override void FixedUpdate ()
    {
        currentPosition = rb.position;
        if (Vector3.Distance(currentPosition, destination) > 0.5f)
            rb.velocity = direction * Time.deltaTime * speed;
        else
            StartCoroutine(Recharge(0.5f));
    }

    private void Update()
    {
        //Rotates the object towards the direction is about to charge in.
        if (direction == Vector3.zero && player)
        {
            Quaternion facingTheChargeDirection = Quaternion.LookRotation((destination - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, facingTheChargeDirection, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Character>().TakeDamage();
    }

    //This function makes the charger wait for "time" seconds, then it tells him to where to charge next.
    //The charger will always charge 5 meters when using this function.
    private IEnumerator Recharge(float time)
    {
        direction = Vector3.zero;
        if (player != null)
        {
            destination = (player.transform.position - currentPosition).normalized * 5f + currentPosition;
            //Make sure the charger doesn't exit the map
            //------------------------------------------------------------
            if (destination.z > boundary)
            {
                if (destination.x < -boundary)
                    destination = new Vector3(-boundary, destination.y, boundary);
                else if (destination.x < boundary)
                {
                    destination.x = destination.x / destination.z * boundary;
                    destination.z = boundary;
                }
                else
                    destination = new Vector3(boundary, destination.y, boundary);
            }
            else if (destination.z < -boundary)
            {
                if (destination.x < -boundary)
                    destination = new Vector3(-boundary, destination.y, -boundary);
                else if (destination.x < boundary)
                {
                    destination.x = destination.x / destination.z * -boundary;
                    destination.z = -boundary;
                }
                else
                    destination = new Vector3(boundary, destination.y, -boundary);
            }
            else
            {
                if (destination.x < -boundary)
                {
                    destination.z = destination.z / destination.x * -boundary;
                    destination.x = -boundary;
                }
                else if (destination.x > boundary)
                {
                    destination.z = destination.z / destination.x * boundary;
                    destination.x = boundary;
                }
            }
            //------------------------------------------------------------
            yield return new WaitForSeconds(time);
            direction = (destination - currentPosition).normalized;
        }
        else
            rb.velocity = Vector3.zero;
    }
}
