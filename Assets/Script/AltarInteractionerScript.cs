using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteractionerScript : MonoBehaviour
{
    private PlayerScript playerScript;

    public PlayerScript PlayerScript { get => playerScript; set => playerScript = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Altar")
        {
            var altar = collision.GetComponent<AltarScript>();
            PlayerScript.PickUp(altar.WeaponObject);
            altar.Interacted();
        }
    }
}
