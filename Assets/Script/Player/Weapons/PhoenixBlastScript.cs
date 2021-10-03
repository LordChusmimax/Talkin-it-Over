using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixBlastScript : FireWeapon
{
    [Header("Gun Values")]
    public Sprite[] laserSprites = new Sprite[4];
    public Sprite[] holeSprites = new Sprite[3];
    public Sprite[] laserEndSprites = new Sprite[4];

    private Animator animator;
    private Transform hole;
    private SpriteRenderer holeRenderer;
    private Transform laser;
    private SpriteRenderer laserRenderer;
    private Transform laserEnd;
    private SpriteRenderer laserEndRenderer;

    private bool trigger;
    private bool justReached;
    private bool shooting;


    RaycastHit2D hit2D;

    public LayerMask layerMask;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Transform[] children = GetComponentsInChildren<Transform>();
        hole = children[1];
        holeRenderer = hole.GetComponent<SpriteRenderer>();
        laser = children[2];
        laserRenderer = laser.GetComponent<SpriteRenderer>();
        laserEnd = children[3];
        laserEndRenderer = laserEnd.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
        trigger = false;
        justReached = false;
        heat = 0;
        overheated = false;
        shooting = false;
        coolingSpeed = 25;
        Aim = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!trigger)
        {
            if (heat > 0)
            {
                LaserUpdate();
                heat -= Time.deltaTime / 3;
            }
            else if (justReached && heat <= 0)
            {
                heat = 0;
                shooting = false;
                overheated = false;
                laserRenderer.enabled = false;
                laserEndRenderer.enabled = false;
                holeRenderer.enabled = false;
                animator.SetTrigger("Cancel");
                Aim = false;
                Debug.Log(heat);
                justReached = false;
            }
            if (Time.frameCount % 10 == 0)
            {
                LaserRandomize();
            }
        }

        if (trigger)
        {
            justReached = true;
            if (heat < 1)
            {
                heat += Time.deltaTime;
            }
            else if (heat >= 1)
            {
                overheated = true;
            }
        }
    }


    public override void onPick()
    {
        positionHandling = new Vector3(0f, 0, 0);
        rotationHandling = new Quaternion(0, 0, 0, 0);
        scaleHandling = new Vector3(1f, 1f, 1f);
        base.onPick();
    }

    public override void Shoot()
    {
       
        if (currentCd <= 0 && !overheated && !shooting)
        {
            animator.SetTrigger("Shoot");
            trigger = true;
            Aim = true;
        }
    }

    public override void Release()
    {
        
        if (overheated && !shooting)
        {
            animator.SetTrigger("Release");
            holeRenderer.sprite = holeSprites[0];
            holeRenderer.enabled = true;
            shooting = true;
            StartCoroutine(Fire());
        } else if (!shooting)
        {
            heat = 0;
            animator.SetTrigger("Cancel");
            Aim = false;
            trigger = false;
        }
    }

    private void LaserUpdate()
    {
        hit2D = Physics2D.Raycast(laser.position, new Vector2(Mathf.Cos(laser.eulerAngles.z * Mathf.PI / 180) * (faceLeft ? 1 : -1), Mathf.Sin(laser.eulerAngles.z * Mathf.PI / 180) * (faceLeft ? 1 : -1)), Mathf.Infinity, layerMask);

        var distance = Vector2.Distance(laser.position, hit2D.point) * 20;
        distance = hit2D.point == Vector2.zero ? 1000 : distance;
        laserRenderer.size = new Vector2(distance, laserRenderer.size.y);

        laserEnd.position = hit2D.point == Vector2.zero ? new Vector2(10000,10000) : hit2D.point;

        if (hit2D.rigidbody != null && hit2D.rigidbody.tag=="Player" && hit2D.rigidbody.gameObject.layer != gameObject.layer)
        {
            hit2D.rigidbody.gameObject.GetComponent<PlayerScript>().Die(true);

            //Registramos la muerte en el sistema de rondas
            ScoreData.addKill(idPlayer);
        } else if (hit2D.rigidbody != null && hit2D.rigidbody.tag == "Enemy")
        {
            hit2D.rigidbody.gameObject.GetComponent<EnemyAI>().Die();
        }
    }
    private void LaserRandomize()
    {
        laserRenderer.sprite = laserSprites[Random.Range(0, 4)];
        laserEndRenderer.sprite = laserEndSprites[Random.Range(0, 4)];
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.15f);
        holeRenderer.sprite = holeSprites[1];

        yield return new WaitForSeconds(0.15f);

        holeRenderer.sprite = holeSprites[2];


        yield return new WaitForSeconds(0.15f);
        animator.SetTrigger("Discharge");

        holeRenderer.sprite = holeSprites[3];
        laserRenderer.enabled = true;
        laserEndRenderer.enabled = true;
        heat = 1;
        trigger = false;
    }
}