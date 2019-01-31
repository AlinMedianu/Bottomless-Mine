using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : CharacterShoot
{
    [SerializeField]
    protected Rigidbody projectilePrefab;

    protected override void Awake()
    {       
        StartCoroutine(Shoot(Vector3.back, Vector3.zero));
    }

    protected override IEnumerator Shoot(Vector3 direction, Vector3 positionOffset)
    {
        Vector3 position = transform.Find("Weapon").position;
        Rigidbody projectile = Instantiate(projectilePrefab, position + positionOffset, Quaternion.LookRotation(direction));
        projectile.velocity = direction * projectileSpeed * Time.deltaTime;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        StartCoroutine(Shoot(direction, positionOffset));
    }
}
