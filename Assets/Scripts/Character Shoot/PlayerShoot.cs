using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : CharacterShoot
{
    private bool firstShot;
    [SerializeField]
    private Rigidbody[] projectilePrefabs;
    [SerializeField]
    private AudioSource ShootSound; //Shoot sound source

    public bool FirstShot
    {
        private get
        {
            return firstShot;
        }
        set
        {
            firstShot = value;
        }
    }
    public Weapon CurrentWeapon { get; set; }  //the player's weapon

    protected override void Awake()
    {
        CurrentWeapon = transform.Find("Weapon").GetComponent<Weapon>();
        //ShootSound.volume = 0.25f;
    }

    private void Update()
    {        
        //executes once every time you press space or when your weapon changes
        if (Input.GetKeyDown(KeyCode.Space) || CurrentWeapon.WeaponChanged)
        {            
            if (CurrentWeapon.WeaponChanged)
                StopAllCoroutines();
            if (Input.GetKey(KeyCode.Space) && (CurrentWeapon.WeaponType != ShootingMode.Detonate || !CurrentWeapon.WeaponChanged))
                switch (CurrentWeapon.WeaponType)
                {
                    case ShootingMode.Normal:
                        StartCoroutine(Shoot(Vector3.forward, Vector3.zero));                  
                        break;
                    case ShootingMode.Double:
                        StartCoroutine(Shoot(Vector3.forward, Vector3.left * 0.1f));
                        StartCoroutine(Shoot(Vector3.forward, Vector3.right * 0.1f));
                        break;
                    case ShootingMode.ThreeWay:
                        StartCoroutine(Shoot(new Vector3(-1, 0, 3).normalized, Vector3.left * 0.1f));
                        StartCoroutine(Shoot(Vector3.forward, Vector3.zero));
                        StartCoroutine(Shoot(new Vector3(1, 0, 3).normalized, Vector3.right * 0.1f));                 
                        break;
                    case ShootingMode.FiveWay:
                        StartCoroutine(Shoot(new Vector3(-Mathf.Sqrt(3), 0, 1).normalized, Vector3.left * 0.2f));
                        StartCoroutine(Shoot(new Vector3(-1, 0, Mathf.Sqrt(3)).normalized, Vector3.left * 0.1f));
                        StartCoroutine(Shoot(Vector3.forward, Vector3.zero));
                        StartCoroutine(Shoot(new Vector3(1, 0, Mathf.Sqrt(3)).normalized, Vector3.right * 0.1f));
                        StartCoroutine(Shoot(new Vector3(Mathf.Sqrt(3), 0, 1).normalized, Vector3.right * 0.2f));           
                        break;
                    case ShootingMode.Follow:
                        StartCoroutine(Shoot(Vector3.forward, Vector3.zero, 1));                  
                        break;
                    case ShootingMode.Detonate:
                        if(!firstShot)
                            StartCoroutine(Shoot(Vector3.forward, Vector3.zero));
                        else
                            StartCoroutine(Shoot(Vector3.forward, Vector3.zero, 2));
                        break;
                }
            CurrentWeapon.WeaponChanged = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
            StopAllCoroutines();
    }

    protected override IEnumerator Shoot(Vector3 direction, Vector3 positionOffset)
    {
        //ShootSound.Play(); //Shoot Sound
        Vector3 position = transform.Find("Weapon").position;
        Rigidbody projectile = Instantiate(projectilePrefabs[0], position + positionOffset, Quaternion.LookRotation(direction));
        projectile.velocity = direction * projectileSpeed * Time.deltaTime;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        StartCoroutine(Shoot(direction, positionOffset));
    }
    private IEnumerator Shoot(Vector3 direction, Vector3 positionOffset, int bulletID)
    {
        //ShootSound.Play(); //Shoot Sound
        Vector3 position = transform.Find("Weapon").position;
        Rigidbody projectile = Instantiate(projectilePrefabs[bulletID], position + positionOffset, Quaternion.LookRotation(direction));
        projectile.velocity = direction * projectileSpeed * Time.deltaTime;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        if(bulletID != 2)
            StartCoroutine(Shoot(direction, positionOffset, bulletID));
        else
            StartCoroutine(Shoot(direction, positionOffset));
    }
}