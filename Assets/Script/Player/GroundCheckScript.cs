using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator playerAnimator;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerAnimator = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Air", false);
                isGrounded = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            playerAnimator.SetBool("Air", true);
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }
}
