using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    [SerializeField] protected float Cadence;

    protected float currentCd;
    
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