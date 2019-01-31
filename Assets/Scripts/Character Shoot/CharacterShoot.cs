using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the class that defines the ranged attacks
public abstract class CharacterShoot : MonoBehaviour
{

    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float timeBetweenProjectiles;

    public float ProJectileSpeed
    {
        private get
        {
            return projectileSpeed;
        }
        set
        {
            projectileSpeed = value;
        }
    }

    protected abstract void Awake();

    protected abstract IEnumerator Shoot(Vector3 direction, Vector3 positionOffset);
}

//the different weapon types
public enum ShootingMode { Normal, Double, ThreeWay, FiveWay, Follow, Detonate }