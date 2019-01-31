using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class was made to facilitate multiple types of enemy movements
public abstract class EnemyNavigate : CharacterNavigate
{
    protected abstract override void FixedUpdate();
}
