using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public int playerIndex;


    private CapsuleCollider2D playerColider;
    private PlayerScript playerScript;
    private Weapon weapon;
    private Rigidbody2D rb;
    private Animator animator;

    [HideInInspector] public CinemachineTargetGroup cmTargerGroup;
    [HideInInspector] public GameObject head;

    void Start()
    {
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
            playerScript.Die(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scenary" && !playerScript.Dead)
        {
            playerScript.Die(false);
        }
    }

}
