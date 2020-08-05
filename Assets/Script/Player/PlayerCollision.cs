using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public int playerIndex;

    private Rigidbody2D[] childrenRB;
    private CapsuleCollider2D[] childrenColliders;
    private CapsuleCollider2D playerColider;
    private PlayerScript playerScript;
    private Weapon weapon;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        childrenRB = GetComponentsInChildren<Rigidbody2D>();
        childrenColliders = GetComponentsInChildren<CapsuleCollider2D>();
        playerScript = GetComponent<PlayerScript>();
        animator = GetComponentInChildren<Animator>();
        weapon = playerScript.weapon;
        rb = GetComponent<Rigidbody2D>();
        playerColider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ammunition")
        {
            Debug.Log(collision.collider.gameObject.layer+"//"+collision.otherCollider.gameObject.layer);
            Die();
        }
    }

    private void Die()
    {
        foreach (Rigidbody2D childRigidBody in childrenRB)
        {
            childRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
        foreach (CapsuleCollider2D capsuleCollider in childrenColliders)
        {
            capsuleCollider.enabled = true;
        }
        playerColider.enabled = false;
        playerScript.enabled = false;
        animator.enabled = false;
        Destroy(weapon.gameObject);
    }

}
