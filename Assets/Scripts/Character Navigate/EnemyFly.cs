using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : EnemyNavigate
{
    [SerializeField]
    private float forwardDistance;              //how much he advances forward with each flap (as he moves diagonally)
    [SerializeField]                            //
    private float sideDistance;                 //how much he advances left or righ with each flap(as he moves diagonally)
    [SerializeField]
    private float unSquareRootedUpDistance;     
    private bool reachedLeft;                   //true after he finished a flap to the left (of the player) and false after he finished a flap to the right (of the player)
    private float startingY;                    //so it can fly no matter the y of the transform
    private float timer;
    private Vector3 currentPosition;
    private Vector3 waypoint;                   //next position
    private Vector3 positionOffset;             //so it doesn't go to infinity and beyond 

    protected override void Awake()
    {
        base.Awake();
        currentPosition = transform.position;
        startingY = transform.position.y;
        reachedLeft = false;
        waypoint = currentPosition + new Vector3(-sideDistance * 0.5f, 0, -forwardDistance);
        positionOffset = rb.position;
    }

    protected override void FixedUpdate()
    {
        //On Screen
        if (rb.position.z > -6.5f)
        {
            //Performing a flap to the left (of the player)
            if (rb.position.x > waypoint.x && rb.position.z > waypoint.z && !reachedLeft)
            {
                if (timer < 1)
                {
                    timer += Time.deltaTime;
                    Vector3 towardsWaypoint = waypoint - currentPosition;
                    rb.position = new Vector3(towardsWaypoint.x * timer, Mathf.Sin(timer * Mathf.PI) * Mathf.Sqrt(unSquareRootedUpDistance) * 0.5f, towardsWaypoint.z * timer) + positionOffset;
                }
            }
            //Finished a flap to the left (of the player)
            else if (rb.velocity == Vector3.zero && !reachedLeft)
            {
                positionOffset = rb.position;
                positionOffset.y = startingY;
                waypoint = rb.position + new Vector3(sideDistance, 0, -forwardDistance);
                reachedLeft = true;
                timer = 0;
                currentPosition = rb.position;
            }
            //Performing a flap to the right (of the player)
            else if (rb.position.x < waypoint.x && rb.position.z > waypoint.z && reachedLeft)
            {
                if (timer < 1)
                {
                    timer += Time.deltaTime;
                    Vector3 towardsWaypoint = waypoint - currentPosition;
                    rb.position = new Vector3(towardsWaypoint.x * timer, Mathf.Sin(timer * Mathf.PI) * Mathf.Sqrt(3.25f) * 0.5f, towardsWaypoint.z * timer) + positionOffset;
                }
            }
            //Finished a flap to the right (of the player)
            else if (rb.velocity == Vector3.zero && reachedLeft)
            {
                positionOffset = rb.position;
                positionOffset.y = startingY;
                waypoint = rb.position + new Vector3(-sideDistance, 0, -forwardDistance);
                reachedLeft = false;
                timer = 0;
                currentPosition = rb.position;
            }
            Moving = true;
        }
        else
            Destroy(gameObject);
    }
}
