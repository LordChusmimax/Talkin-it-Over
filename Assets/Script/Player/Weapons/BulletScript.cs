using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class BulletScript : MonoBehaviour
{
    public bool faceLeft;

    void Start()
    {

        var bulletRB = GetComponent<Rigidbody2D>();
        var angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        if (!faceLeft)
        {
            angle += (float)Math.PI;
            transform.localScale *= new Vector2(-1, 1);
        }
        bulletRB.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * BulletVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Scenary")
        {
            Destroy(this.gameObject);
        }
    }
} 

