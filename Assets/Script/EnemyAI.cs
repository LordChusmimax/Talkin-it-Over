using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cinemachine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    private int currentWayPoint;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator animator;
    public DroneWeaponScript droneWeaponScript;

    private Collider2D weaponCollider;
    private Rigidbody2D weaponRigidbody;
    private HingeJoint2D weaponJoint;
    public bool dead;

    private GameObject[] players;
    private PlayerScript[] playerScripts;

    private CinemachineTargetGroup cmTargetGroup;
    private float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        var weapon = GetComponentsInChildren<Transform>()[1];
        weaponCollider = weapon.GetComponent<Collider2D>();
        weaponRigidbody = weapon.GetComponent<Rigidbody2D>();
        weaponJoint = weapon.GetComponent<HingeJoint2D>();

        players = GameObject.FindGameObjectsWithTag("Player");
        playerScripts = new PlayerScript[players.Length];
        for (int i=0 ; i<players.Length; i++)
        {
            playerScripts[i] = players[i].GetComponent<PlayerScript>();
        }

        cmTargetGroup = GameObject.Find("CM TargetGroup").GetComponent<CinemachineTargetGroup>();
        cmTargetGroup.AddMember(transform,1,4);

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        ChooseTarget();

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        if ((targetDistance > 8 || !droneWeaponScript.aimingTarget) && target != null) rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        UpdateRotation();
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target!= null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void ChooseTarget()
    {
        target = null;
        droneWeaponScript.target = null;
        targetDistance = 1000f;
        for (int i = 0; i < players.Length; i++)
        {
            var newDistance = Vector2.Distance(transform.position, players[i].transform.position);
            if (newDistance < targetDistance && !playerScripts[i].Dead)
            {
                targetDistance = newDistance;
                target = players[i].transform;
                droneWeaponScript.target = players[i];
            }
        }
        if (targetDistance < 20 || target == null)
        {

        }
    }

    void UpdateRotation()
    {

        if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 5f);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

    }

    public void Die()
    {
        if (!dead)
        {
            droneWeaponScript.Die();
            seeker.enabled = false;
            rb.gravityScale = 2f;
            animator.enabled = false;

            weaponCollider.enabled = true;
            weaponJoint.enabled = true;
            weaponRigidbody.bodyType = RigidbodyType2D.Dynamic;

            cmTargetGroup.RemoveMember(transform);

            dead = true;
            enabled = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ammunition")
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lethal")
        {
            Die();
        }
    }
}
