using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public bool faceLeft;
    [HideInInspector] public float range;
    [HideInInspector] public int idPlayer;

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
        StartCoroutine(RangeEnforcer());

    }

    private IEnumerator RangeEnforcer()
    {
        var time = range / BulletVelocity;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer != 17)
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

