using System;
using System.Collections;
using UnityEngine;

public class Shotgun : FireWeapon
{
    [Header("ShotGun Values")]
    [SerializeField] private GameObject bulletGameObject;
    private Transform hole;
    private Animator animator;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hole = GetComponentsInChildren<Transform>()[1];
        animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void onPick()
    {
        positionHandling = new Vector3(0f, 0f, 0);
        rotationHandling = new Quaternion(0, 0, 0, 0);
        scaleHandling = new Vector3(1f, 1f, 1);
        base.onPick();
    }

    public override void Shoot()
    {
        if (currentCd <= 0)
        {
            attackSound.Play();
            animator.SetTrigger("Shoot");
            currentCd = cadence;
            for (int i = 0; i < 6; i++)
            {
                CreateBullet();
            }
        }
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
        bulletScript.gameObject.layer = gameObject.layer + 1;
        bulletScript.enabled = true;
    }


}
