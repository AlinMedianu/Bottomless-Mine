using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : CharacterNavigate
{
    [SerializeField]                            //keep the player in a box
    private float leftLimit;                    //
    [SerializeField]                            //
    private float rightLimit;                   //
    [SerializeField]                            //
    private float upperLimit;                   //
    [SerializeField]                            //
    private float lowerLimit;                   //
    private bool reachedLeft = false;           //
    private bool reachedRight = false;          //
    private bool reachedUp = false;             //
    private bool reachedDown = false;           //

    protected override void FixedUpdate()
    {
        //move forward
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !reachedUp)
        {
            Moving = true;
            rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        }
        //move to the right
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !reachedLeft)
        {
            Moving = true;
            rb.velocity = new Vector3(-1 * speed * Time.deltaTime, 0, 0);
        }
        //move to the left
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !reachedDown)
        {
            Moving = true;
            rb.velocity = new Vector3(0, 0, -1 * speed * Time.deltaTime);
        }
        //move backwards
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !reachedRight)
        {
            Moving = true;
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        //idle
        else
        {
            Moving = false;
            rb.velocity = Vector3.zero;
        }
        reachedUp = rb.position.z > upperLimit;
        reachedLeft = rb.position.x < leftLimit;
        reachedDown = rb.position.z < lowerLimit;
        reachedRight = rb.position.x > rightLimit;
    }
}
