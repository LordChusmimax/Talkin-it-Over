using System;
using System.Collections;
using UnityEngine;

public class Gun : FireWeapon
{
    [Header("Gun Values")]
    [SerializeField] private GameObject bulletGameObject;
    private Transform hole;


    // Start is called before the first frame update
    void Start()
    {
        hole = GetComponentsInChildren<Transform>()[1];

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
        if (currentCd <= 0)
        {
            currentCd = cadence;
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        var bulletDispersion = (UnityEngine.Random.Range(-dispersion, dispersion));
        var bullet = Instantiate(bulletGameObject);
        bullet.transform.position = hole.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(bulletDispersion*Vector3.forward);
        var bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.range = range;
        bulletScript.faceLeft = faceLeft;
        bulletScript.gameObject.layer = gameObject.layer;
        bulletScript.enabled=true;
    }


}
