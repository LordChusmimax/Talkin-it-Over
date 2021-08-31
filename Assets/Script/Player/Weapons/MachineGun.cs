using System;
using System.Collections;
using UnityEngine;

public class MachineGun : FireWeapon
{
    [Header("Gun Values")]
    [SerializeField] private GameObject bulletGameObject;
    private Animator animator;
    private Transform hole;
    private bool trigger;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hole = GetComponentsInChildren<Transform>()[1];
        animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
        trigger = false;
        heat = 0;
        overheated = false;
        coolingSpeed = 25;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckHeat();
        CheckColor();
    }

    private void CheckHeat()
    {
        if (heat >= 100)
        {
            heat = 100;
            overheated = true;
            Release();
        }
        if (heat > 0)
        {
            heat -= Time.deltaTime * coolingSpeed;
        }
        else
        {
            overheated = false;
            heat = 0;
        }
    }

    private void CheckColor()
    {
        spriteRenderer.color = new Color(1, 1-heat/100, 1 - heat/100, 1);
    }

    public override void onPick()
    {
        positionHandling = new Vector3(0f, 0, 0);
        rotationHandling = new Quaternion(0, 0, 0, 0);
        scaleHandling = new Vector3(1f, 1f, 1);
        base.onPick();
    }

    public override void Shoot()
    {
        if (currentCd <= 0 && !overheated)
        {
            animator.ResetTrigger("Release");
            animator.SetTrigger("Shoot");
            trigger = true;
            KeepShooting();
        }
    }

    public void KeepShooting()
    {
        attackSound.Play();
        currentCd = cadence;
        CreateBullet();
        heat += 10;
        StartCoroutine(ShootCadence());
    }

    public override void Release()
    {
        animator.SetTrigger("Release");
        trigger = false;
    }

    private void CreateBullet()
    {
        var bulletDispersion = (UnityEngine.Random.Range(-dispersion, dispersion));
        var bullet = Instantiate(bulletGameObject);
        bullet.transform.position = hole.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(bulletDispersion * Vector3.forward);

        var bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.range = range;
        bulletScript.faceLeft = faceLeft;
        bulletScript.gameObject.layer = gameObject.layer;
        bulletScript.enabled = true;
    }

    IEnumerator ShootCadence()
    {
        yield return new WaitForSeconds(cadence);
        if (trigger) { KeepShooting(); };
    }


}
