using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField]
    private float homingProjectileVelocity;
    private static int previousNumberOfEnemies;
    private float distanceBetweenPlayerAndClosestEnemy;
    private GameObject closestEnemy;
    private static GameObject[] enemies;

    protected override void Start()
    {
        base.Start();
        previousNumberOfEnemies = 0;
        distanceBetweenPlayerAndClosestEnemy = float.MaxValue;
        closestEnemy = null;
    }

    protected virtual void Update()
    {
        if (previousNumberOfEnemies != Spawner.NumberOfEnemies)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");       //updates the array only when the number of enemies changed
            previousNumberOfEnemies = Spawner.NumberOfEnemies;
        }
        if (enemies != null)
            foreach (GameObject enemy in enemies)
                if (enemy && Vector3.Distance(enemy.transform.position, transform.position) < distanceBetweenPlayerAndClosestEnemy)
                {
                    distanceBetweenPlayerAndClosestEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                    closestEnemy = enemy;
                }
    }

    private void FixedUpdate()
    {
        if (distanceBetweenPlayerAndClosestEnemy != float.MaxValue && closestEnemy)
        {
            rb.velocity = (closestEnemy.transform.position - transform.position).normalized * homingProjectileVelocity * Time.deltaTime;
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}
