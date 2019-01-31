using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonatableProjectile : Projectile
{
    [SerializeField]
    private int areaDamage;
    [SerializeField]
    private float areaRadius;
    private GameObject[] enemies;

    protected override void Start()
    {
        base.Start();
        FindObjectOfType<PlayerShoot>().FirstShot = false;
        FindObjectOfType<PlayerShoot>().ProJectileSpeed = 500f;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            bool ignored = false;
            foreach (string tag in ignoreTags)
                if (other.tag == tag)
                    ignored = true;
            if(other.tag == "Projectile")
            {
                if(other.name == "Projectile(Clone)")
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    Explode();
                }
            }
            else if (!ignored)
            {
                other.GetComponent<Character>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    private void Explode()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            if (Vector3.Distance(transform.position, enemy.transform.position) <= areaRadius)
                enemy.GetComponent<Enemy>().TakeDamage(areaDamage);
    }
}
