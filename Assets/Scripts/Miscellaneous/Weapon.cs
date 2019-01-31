using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IBlink
{
    [SerializeField]
    private float blinkTimer;                       //the weapon is getting close to the moment it disappears
    [SerializeField]
    private float selfDestructionTimer;             //the moment the weapon disappears
    [SerializeField]
    private float resetTimer;                       //the moment the weapon's effect runs out
    [SerializeField]
    private ShootingMode weaponType;
    private bool isEquiped;
    private bool done;
    private float timer;
    private MeshRenderer rend;

    public bool WeaponChanged { get; set; }
    public Color OriginalColor { get; set; }
    public Color InvertedColor { get; set; }

    public Color CurrentColor
    {
        get
        {
            return rend.material.color;
        }
        set
        {
            rend.material.color = value;
        }
    }

    public ShootingMode WeaponType
    {
        get
        {
            return weaponType;
        }
        set
        {
            if(weaponType != value && value != ShootingMode.Normal)
                WeaponChanged = true;
            if (value != ShootingMode.Detonate)
                FindObjectOfType<PlayerShoot>().ProJectileSpeed = 300f;
            else
            {
                FindObjectOfType<PlayerShoot>().FirstShot = true;
                FindObjectOfType<PlayerShoot>().ProJectileSpeed = 150f;
            }
                weaponType = value;
            timer = 0;
        }
    }

    private void Start()
    {
        done = false;
        isEquiped = transform.parent;
        timer = 0;
        if (!isEquiped)
        {
            rend = transform.Find("Animation").Find("Body").GetComponent<MeshRenderer>();
            CurrentColor = rend.material.color;
            OriginalColor = CurrentColor;
            InvertedColor = new Color(1, 1, 1, 0) - CurrentColor;
            foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag("PowerUp")) //no two weapons can be on the map at the same time
                if(powerUp.name == "Weapon" && powerUp != gameObject)
                    Destroy(powerUp);
        }
        else
            WeaponChanged = false;

    }

    private void Update()
    {
        if (!isEquiped)
        {
            timer += Time.deltaTime;
            if (timer > selfDestructionTimer)
                Destroy(gameObject);
            else if (timer > blinkTimer && !done)
            {
                StartCoroutine(BlinkColor());
                done = true;
            }
        }
        else if(WeaponType != ShootingMode.Normal)
        {
            timer += Time.deltaTime;
            if (timer > resetTimer)
                WeaponType = ShootingMode.Normal;
        }
    }

    public IEnumerator BlinkColor()
    {
        CurrentColor = InvertedColor;
        yield return new WaitForSeconds(0.1f);
        CurrentColor = OriginalColor;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BlinkColor());       
    }
}
