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
        hole = GetComponentsInChildren<Transform>()[1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void onPick()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        CreateBullet();
    }

    private void CreateBullet()
    {
        var bullet = Instantiate(bulletGameObject);
        bullet.transform.position = hole.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<BulletScript>().faceLeft = faceLeft;
        bullet.GetComponent<BulletScript>().enabled=true;
    }


}
