using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private Vector3 powerUpSpawnLocation;       //the boss will spawn a power up at this location 3 times
    private bool firstPowerUpSpawned;           //bools to make sure it spawns the power ups just once after hitting each threshold
    private bool secondPowerUpSpawned;          //bools to make sure it spawns the power ups just once after hitting each threshold
    private bool thirdPowerUpSpawned;           //bools to make sure it spawns the power ups just once after hitting each threshold
    private Transform player;                   //for the enemy to look at the player

    protected override void Awake()
    {
        base.Awake();
        rb.isKinematic = true;
        firstPowerUpSpawned = false;
        secondPowerUpSpawned = false;
        thirdPowerUpSpawned = false;
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        transform.LookAt(player);
        //the three thresholds
        //-------------------------------------------------------------------------------------------
        if (Health == 75  && !firstPowerUpSpawned)
        {
            Transform weapon = Instantiate(powerUpPrefab, powerUpSpawnLocation, Quaternion.identity);
            weapon.GetComponent<Weapon>().WeaponType = (ShootingMode)Random.Range(1, 5);
            weapon.name = powerUpPrefab.name;
            firstPowerUpSpawned = true;
        }
        else if (Health == 50 && !secondPowerUpSpawned)
        {
            Transform weapon = Instantiate(powerUpPrefab, powerUpSpawnLocation, Quaternion.identity);
            weapon.GetComponent<Weapon>().WeaponType = (ShootingMode)Random.Range(1, 5);
            weapon.name = powerUpPrefab.name;
            secondPowerUpSpawned = true;
        }
        else if (Health == 25 && !thirdPowerUpSpawned)
        {
            Transform weapon = Instantiate(powerUpPrefab, powerUpSpawnLocation, Quaternion.identity);
            weapon.GetComponent<Weapon>().WeaponType = (ShootingMode)Random.Range(1, 5);
            weapon.name = powerUpPrefab.name;
            thirdPowerUpSpawned = true;
        }
        //-------------------------------------------------------------------------------------------
    }

    protected override void OnDestroy()
    {
        Spawner.NumberOfEnemies--;
    }
}
