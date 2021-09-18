using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireWeapon : Weapon
{
    [SerializeField] protected int dispersion;
    [SerializeField] protected int burst;
    [SerializeField] protected float range;
    [SerializeField] protected float heat;
    [SerializeField] protected int coolingSpeed;
    [SerializeField] protected bool overheated;
}
