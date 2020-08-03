using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    protected float cadence;
    protected int dispersion;
    protected int burst;
    protected float cd;
    
    [HideInInspector]public bool faceLeft;

    public abstract void onPick();

    public abstract void Shoot();
}