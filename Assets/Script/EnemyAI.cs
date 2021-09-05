using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    public Collider2D weaponCollider;
    public Rigidbody2D weaponRigidbody;
    public HingeJoint2D weaponJoint;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

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

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        UpdateRotation();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        droneWeaponScript.Die();
        Debug.Log("dead");
        seeker.enabled = false;
        rb.gravityScale = 1.5f;
        animator.enabled = false;

        weaponCollider.enabled = true;
        weaponJoint.enabled = true;
        weaponRigidbody.bodyType = RigidbodyType2D.Dynamic;


        enabled = false;
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
