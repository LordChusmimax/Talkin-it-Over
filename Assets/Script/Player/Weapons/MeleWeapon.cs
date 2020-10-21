using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleWeapon : Weapon
{
    [SerializeField] protected float startFrames;
    [SerializeField] protected float hitFrames;
    protected Collider2D hurtBox;

    protected abstract void PreHit();
    protected abstract void HitStart();
    protected abstract void PosHit();
    protected abstract void BackToCalm();


    public override void Shoot()
    {
        if (currentCd <= 0)
        {
            StartCoroutine(Hit());
            currentCd = cadence;
        }
    }

    public IEnumerator Hit()
    {
        PreHit();
        if (startFrames != 0)
        {
            yield return new WaitForSeconds(startFrames);
        }
        else
        {
            yield return new WaitForFixedUpdate();
        }
        HitStart();
        yield return new WaitForSeconds(hitFrames);
        PosHit();
        yield return new WaitForSeconds(cadence-hitFrames-startFrames);
        BackToCalm();
    }


}
