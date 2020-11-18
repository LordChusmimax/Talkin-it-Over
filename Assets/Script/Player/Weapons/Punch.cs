using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MeleWeapon
{
    private Animator animator;
    private PunchHurtBox hurtBoxScript;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        hurtBox = GetComponentsInChildren<Collider2D>()[1];
        hurtBoxScript = GetComponentInChildren<PunchHurtBox>();
        hurtBoxScript.Faceleft = faceLeft;
        attackSound = GetComponent<AudioSource>();
    }

    public override void Update()
    {
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
        attackSound.Play();
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
