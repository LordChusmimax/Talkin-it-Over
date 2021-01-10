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
        weapon = playerScript.Weapon;
        rb = GetComponent<Rigidbody2D>();
        playerColider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ammunition" && !playerScript.Dead)
        {
            playerScript.Die(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "NonLethal" && !playerScript.Dead)
        {
            playerScript.Stun(3);
        }
        else if (collision.tag == "Lethal" && !playerScript.Dead)
        {
            playerScript.Die(true);
        }
        else if (collision.tag == "Scenary" && !playerScript.Dead)
        {
            playerScript.Die(false);
        }
    }

}
