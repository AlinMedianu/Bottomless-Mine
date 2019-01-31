using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : EnemyShoot
{
    protected override void Awake()
    {
        StartCoroutine(Shoot(Vector3.zero));
    }

    //so the boss shoots in its forward direction, which is changing, goes it is always looking at the player
    private IEnumerator Shoot(Vector3 positionOffset)
    {
        Vector3 position = transform.Find("Weapon").position;
        Rigidbody projectile = Instantiate(projectilePrefab, position + positionOffset, Quaternion.LookRotation(transform.forward));
        projectile.velocity = transform.forward * projectileSpeed * Time.deltaTime;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        StartCoroutine(Shoot(positionOffset));
    }
}
