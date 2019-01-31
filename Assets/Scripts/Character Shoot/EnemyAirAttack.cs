using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirAttack : EnemyShoot
{
    [SerializeField]
    private float shootingDistance;
    private bool behind;                //true if the player is behind the flying enemy
    private Transform playerPosition;
    protected override void Awake()
    {
        if(GameObject.Find("Player"))
        {
            playerPosition = GameObject.Find("Player").transform;
            StartCoroutine(Shoot(new Vector3(transform.position.x, playerPosition.position.y, playerPosition.position.z) - transform.position, Vector3.zero));
        }
    }

    private void Update()
    {
        if (behind && GameObject.Find("Player"))
        {
            if (transform.position.z - playerPosition.position.z > shootingDistance)
                StartCoroutine(Shoot(new Vector3(transform.position.x, playerPosition.position.y, playerPosition.position.z) - transform.position, Vector3.zero));
        }
    }

    //shoot towards the horizontal line the player is in, unless the player is behind the flying enemy
    protected override IEnumerator Shoot(Vector3 direction, Vector3 positionOffset)
    {
        behind = false;
        Vector3 position = transform.Find("Weapon").position;
        Rigidbody projectile = Instantiate(projectilePrefab, position + positionOffset, Quaternion.LookRotation(direction));
        projectile.velocity = new Vector3(direction.x, direction.y, direction.z) * projectileSpeed * Time.deltaTime;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        if (transform.position.z - playerPosition.position.z > shootingDistance)
            StartCoroutine(Shoot(new Vector3(transform.position.x, playerPosition.position.y, playerPosition.position.z) - transform.position, Vector3.zero));
        else
            behind = true;
    }
}
