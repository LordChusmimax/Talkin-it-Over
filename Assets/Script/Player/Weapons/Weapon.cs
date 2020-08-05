using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    protected float cadence;
    protected float currentCd;
    protected float maxCd;
    
    [HideInInspector]public bool faceLeft;

    protected virtual void Update()
    {
        HandleCd();
    }

    public abstract void onPick();

    public abstract void Shoot();

    public void HandleCd()
    {
        if (currentCd > 0)
        {
            currentCd -= Time.deltaTime;
        }
    }
}