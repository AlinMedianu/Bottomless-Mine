using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Allows Scenes to be loaded

public class Player : Character
{


    private bool invulnerable = false;                  //The player is invulnerable after getting hit
    [SerializeField]                                    //
    private float invulnerableTimer;                    //
    private float timer;                                //
    [SerializeField]
    private Canvas gameOverMenu; // Creates Canvas Object
    private AudioSource soundSystem;
    [SerializeField]
    private AudioClip weaponPickUpSound; //Weapon Pick Up sound source
    [SerializeField]
    private AudioClip heartPickUpSound; //Weapon Pick Up sound source
    [SerializeField]
    private AudioClip damageSound; //Damage sound source

    protected override void Awake()
    {
        base.Awake();
        soundSystem = GetComponent<AudioSource>();
        soundSystem.Play();
    }
    private void Update()
    {
        if(invulnerable)
        {
            timer -= Time.deltaTime;
            if(timer < 0)                               //When the invulnerability has expired
            {
                invulnerable = false;
                StopAllCoroutines();
                CurrentColor = OriginalColor;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.root.name == "Weapon")       //When picking up a weapon
        {
            transform.Find("Weapon").GetComponent<Weapon>().WeaponType = other.transform.root.GetComponent<Weapon>().WeaponType;
            Destroy(other.transform.root.gameObject);
            soundSystem.clip = weaponPickUpSound;   //PickUP Sound
            soundSystem.Play(); 
        }
        else if(other.name == "Heart")                  //When picking up a heart
        {
            Health++;
            Destroy(other.gameObject);
            soundSystem.clip = heartPickUpSound;    //PickUP Sound
            soundSystem.Play(); 
        }
    }

    protected override void OnDestroy()
    {
        if (Health == 0)
        {
            gameOverMenu.enabled = true; // Shows GameOver Screen when player's health reaches 0
        }
    }

    public override void TakeDamage()
    {
        if(!invulnerable)
        {
            base.TakeDamage();
            timer = invulnerableTimer;
            invulnerable = true;
            if(soundSystem.enabled)
            {
                soundSystem.clip = damageSound;     //Damage Sound
                soundSystem.Play(); 
            }
        }
    }

    public override void TakeDamage(int amount)
    {
        if (!invulnerable && amount > 0)
        {
            base.TakeDamage(amount);
            timer = invulnerableTimer;
            invulnerable = true;
            if (soundSystem.enabled)
            {
                soundSystem.clip = damageSound;     //Damage Sound
                soundSystem.Play();
            }
        }
    }

    /// <summary>
    /// Allows the player to visualize when he/she got hit
    /// </summary>
    /// <returns></returns>
    public override IEnumerator BlinkColor()
    {
        CurrentColor = InvertedColor;
        yield return new WaitForSeconds(0.1f);
        CurrentColor = OriginalColor;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BlinkColor());
    }

}
