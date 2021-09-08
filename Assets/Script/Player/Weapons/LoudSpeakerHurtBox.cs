using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudSpeakerHurtBox : MonoBehaviour
{
    private bool faceleft;

    public bool Faceleft { get => faceleft; set => faceleft = value; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerScript>() == null || !collider.GetComponent<PlayerScript>().StunResistant)
        {
            if (collider.attachedRigidbody != null)
            {
                collider.attachedRigidbody.AddForce(new Vector3((faceleft ? 1000 : -1000), 0, 0));
            }
        }
    }
}
