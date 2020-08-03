using System;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{

    [SerializeField] private GameObject bulletGameObject;
    private Transform hole;


    // Start is called before the first frame update
    void Start()
    {
        maxCd = 0.5f;
        dispersion = 5;
        hole = GetComponentsInChildren<Transform>()[1];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void onPick()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        if (currentCd <= 0)
        {
            currentCd = maxCd;
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        var bulletDispersion = (UnityEngine.Random.Range(-dispersion, dispersion));
        Debug.Log(bulletDispersion);
        var bullet = Instantiate(bulletGameObject);
        bullet.transform.position = hole.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(bulletDispersion*Vector3.forward);
        bullet.GetComponent<BulletScript>().faceLeft = faceLeft;
        bullet.GetComponent<BulletScript>().enabled=true;
    }


}
