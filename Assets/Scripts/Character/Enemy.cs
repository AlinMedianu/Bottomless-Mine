using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    protected Transform powerUpPrefab;

    protected override void OnDestroy()
    {
        //power up spawning section
        //-------------------------------------------------------------------------------------------------------------------------------------------
        if(health == 0 && GameObject.Find("Player"))
        {
            if (name == "Wisp")
            {
                if (Random.Range(0, 2) == 0)
                {
                    Transform heart = Instantiate(powerUpPrefab, new Vector3(transform.position.x, 0.25f, transform.position.z), Quaternion.identity);
                    heart.name = powerUpPrefab.name;
                }
            }
            else if (name == "Spider")
            {
                Transform weapon = Instantiate(powerUpPrefab, new Vector3(transform.position.x, 0.25f, transform.position.z), Quaternion.identity);
                weapon.name = powerUpPrefab.name;
                if (Random.Range(0, 2) == 0)
                    weapon.GetComponent<Weapon>().WeaponType = ShootingMode.Follow;
                else
                    weapon.GetComponent<Weapon>().WeaponType = ShootingMode.Detonate;
            }
            else 
            {
                Transform weapon = Instantiate(powerUpPrefab, new Vector3(transform.position.x, 0.25f, transform.position.z), Quaternion.identity);
                weapon.name = powerUpPrefab.name;
                weapon.GetComponent<Weapon>().WeaponType = (ShootingMode)Random.Range(1, 5);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------
        Spawner.NumberOfEnemies--;
    }

    /// <summary>
    /// Allows the player to visualize when an enemy was hit
    /// </summary>
    /// <returns></returns>
    public override IEnumerator BlinkColor()
    {
        CurrentColor = InvertedColor;
        yield return new WaitForSeconds(0.1f);
        CurrentColor = OriginalColor;
    }

}
