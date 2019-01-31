using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyNavigate
{
    [SerializeField]
    [Range(0, 1)]
    private float movementOffset;   
    protected GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
    }

    protected override void FixedUpdate()
    {

        if (player)
        {
            //When on the left of the player
            if (transform.position.x - player.transform.position.x < -movementOffset)
            {
                Moving = true;
                rb.velocity = Vector3.right * speed * Time.deltaTime;
            }
            //When on the right of the player
            else if (transform.position.x - player.transform.position.x > movementOffset)
            {
                Moving = true;
                rb.velocity = Vector3.left * speed * Time.deltaTime;
            }
            //When it is in front of the player
            else
            {
                Moving = false;
                rb.velocity = Vector3.zero;
            }
        }
        else
            rb.velocity = Vector3.zero;
    }
}
