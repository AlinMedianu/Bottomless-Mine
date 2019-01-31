using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : Projectile
{
    [SerializeField]
    private float projectileOffset;                 //how far the projectiles are from the center of the boss projectile
    [SerializeField]
    private float projectilesVelocity;
    [SerializeField]
    private Rigidbody projectilePrefab;
    private float explodeDistance;
    private float distanceFromBoss;
    private Transform skull;

    protected override void Start()
    {
        base.Start();
        skull = GameObject.Find("Skull").transform;
        explodeDistance = Random.Range(1f, 3.5f);
    }

    private void Update()
    {
        if(skull)
            distanceFromBoss = Vector3.Distance(transform.position, skull.position);
        if (distanceFromBoss > explodeDistance)
            Explode();
    }

    //picks an explosion type randomly
    private void Explode()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
            ExplodeInAPlus();
        else
            ExplodeInACross();
    }

    //plus '+'
    private void ExplodeInAPlus()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Rigidbody forwardProjectile = Instantiate(projectilePrefab, new Vector3(x, y, z + projectileOffset), Quaternion.LookRotation(Vector3.forward));
        forwardProjectile.velocity = Vector3.forward * projectilesVelocity * Time.deltaTime;
        Rigidbody leftProjectile = Instantiate(projectilePrefab, new Vector3(x - projectileOffset, y, z), Quaternion.LookRotation(Vector3.left));
        leftProjectile.velocity = Vector3.left * projectilesVelocity * Time.deltaTime;
        Rigidbody backProjectile = Instantiate(projectilePrefab, new Vector3(x, y, z - projectileOffset), Quaternion.LookRotation(Vector3.back));
        backProjectile.velocity = Vector3.back * projectilesVelocity * Time.deltaTime;
        Rigidbody rightProjectile = Instantiate(projectilePrefab, new Vector3(x + projectileOffset, y, z), Quaternion.LookRotation(Vector3.right));
        rightProjectile.velocity = Vector3.right * projectilesVelocity * Time.deltaTime;
        Destroy(gameObject);
    }

    //cross 'x'
    private void ExplodeInACross()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Rigidbody forwardLeftProjectile = Instantiate(projectilePrefab, new Vector3(x - projectileOffset * 0.5f, y, z + projectileOffset * 0.5f), 
            Quaternion.LookRotation((Vector3.forward + Vector3.left).normalized));
        forwardLeftProjectile.velocity = (Vector3.forward + Vector3.left).normalized * projectilesVelocity * Time.deltaTime;
        Rigidbody forwardRightProjectile = Instantiate(projectilePrefab, new Vector3(x + projectileOffset * 0.5f, y, z + projectileOffset * 0.5f), 
            Quaternion.LookRotation((Vector3.forward + Vector3.right).normalized));
        forwardRightProjectile.velocity = (Vector3.forward + Vector3.right).normalized * projectilesVelocity * Time.deltaTime;
        Rigidbody backRightProjectile = Instantiate(projectilePrefab, new Vector3(x + projectileOffset * 0.5f, y, z - projectileOffset * 0.5f),
            Quaternion.LookRotation((Vector3.back + Vector3.right).normalized));
        backRightProjectile.velocity = (Vector3.back + Vector3.right).normalized * projectilesVelocity * Time.deltaTime;
        Rigidbody backLeftProjectile = Instantiate(projectilePrefab, new Vector3(x - projectileOffset * 0.5f, y, z - projectileOffset * 0.5f),
            Quaternion.LookRotation((Vector3.back + Vector3.left).normalized));
        backLeftProjectile.velocity = (Vector3.back + Vector3.left).normalized * projectilesVelocity * Time.deltaTime;
        Destroy(gameObject);
    }
}
