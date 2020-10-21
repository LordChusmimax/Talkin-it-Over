using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleWeapon
{
    private Animator animator;
    private SwordHurtBox hurtBoxScript;

    public override void onPick()
    {
        positionHandling = new Vector3(0f, 0, 0);
        rotationHandling = new Quaternion(0, 0, 0, 0);
        scaleHandling = new Vector3(1f, 1f, 1);
        base.onPick();
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        hurtBox = GetComponentsInChildren<Collider2D>()[1];
        hurtBoxScript = GetComponentInChildren<SwordHurtBox>();
        hurtBoxScript.Faceleft = faceLeft;
    }

    public override void Update()
    {
        hurtBoxScript.Faceleft = faceLeft;
        hurtBoxScript.Faceleft = faceLeft;
        base.Update();
    }

    protected override void BackToCalm()
    {
        animator.SetBool("PostHit", false);
        animator.SetBool("BackToCalm", true);
    }

    protected override void HitStart()
    {
        animator.SetBool("PreHit", false);
        animator.SetBool("HitStart", true);
        hurtBox.enabled = true;
    }

    protected override void PosHit()
    {
        animator.SetBool("HitStart", false);
        animator.SetBool("PostHit", true);
        hurtBox.enabled = false;
    }

    protected override void PreHit()
    {
        animator.SetBool("BackToCalm", false);
        animator.SetBool("PreHit", true);
    }

    public override void SetLayer(int layer)
    {
        base.SetLayer(layer);
        hurtBox.gameObject.layer = gameObject.layer;
    }
}
