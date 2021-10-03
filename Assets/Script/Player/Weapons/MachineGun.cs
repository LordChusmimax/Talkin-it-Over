using System;
using System.Collections;
using UnityEngine;

public class MachineGun : FireWeapon
{
    [Header("Gun Values")]
    [SerializeField] private GameObject bulletGameObject;
    [SerializeField] private GameObject smoke;
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
            var smokeEntity = GameObject.Instantiate(smoke,transform.position, transform.rotation);
            if (!faceLeft) smokeEntity.transform.localScale = new Vector3(-smokeEntity.transform.localScale.x, smokeEntity.transform.localScale.y, 0);
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
        heat += heatPerShot;
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
        bulletScript.idPlayer = idPlayer;
        bulletScript.gameObject.layer = gameObject.layer+1;
        bulletScript.enabled = true;
    }

    IEnumerator ShootCadence()
    {
        yield return new WaitForSeconds(cadence);
        if (trigger) { KeepShooting(); };
    }


}
