using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the class that defines all the living objects
[RequireComponent(typeof(Rigidbody))]
public abstract class Character : MonoBehaviour, IBlink
{
    [SerializeField]
    protected int health;
    protected Rigidbody rb;                     
    protected List<Renderer> bodyParts;         //the meshes of the character

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public Color OriginalColor { get; set; }    //used in the blinking effect
    public Color InvertedColor { get; set; }    //used in the blinking effect

    public Color CurrentColor                   //used in the blinking effect
    {
        get
        {
            return bodyParts[0].material.color;
        }

        set
        {
            foreach (Renderer bodyPart in bodyParts)
                bodyPart.material.color = value;
        }
    }

    protected virtual void Awake()
    {
        bodyParts = transform.Find("Model").GetBodyParts();     //extension method that returns the renderers of all the children of a transform
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        OriginalColor = CurrentColor;                          
        InvertedColor = new Color(1, 1, 1, 0) - CurrentColor;
    }

    protected abstract void OnDestroy();

    public virtual void TakeDamage()
    {
        if (health > 0)
            health--;
        else
        {
            health = 0;
            Destroy(gameObject);
        }
        StartCoroutine(BlinkColor());
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            if (health - amount > 0)
                health -= amount;
            else
            {
                health = 0;
                Destroy(gameObject);
            }
            StartCoroutine(BlinkColor());
        }
    }

    public abstract IEnumerator BlinkColor();   
}
