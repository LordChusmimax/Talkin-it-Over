using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    Vector3 initPosition;
    Quaternion initRotation;
    Rigidbody2D rb;
    HingeJoint2D hinge;
    Collider2D collider;
    void Start()
    {
        initPosition = transform.localPosition;
        initRotation = transform.localRotation;
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        collider = GetComponentInChildren<Collider2D>();
    }

    public void EnableRagdoll(bool letLoose)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        hinge.enabled = letLoose ? false : true;
        if (collider != null)
            collider.enabled = true;
    }

    public void DisableRagdoll()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        //transform.localPosition = initPosition;
        StartCoroutine(Undoing());
        transform.localRotation = initRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        if (collider != null)
            collider.enabled = false;
        hinge.enabled = false;
    }

    private IEnumerator Undoing()
    {
        Vector3 positionStep = initPosition - transform.localPosition;
        positionStep = (positionStep * 2f) * Time.deltaTime;
        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            transform.localPosition += positionStep;
        }
        transform.localPosition = initPosition;
    }
}
