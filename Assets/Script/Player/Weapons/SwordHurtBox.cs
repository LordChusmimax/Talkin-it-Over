using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHurtBox : MonoBehaviour
{
    private bool faceleft;

    public bool Faceleft { get => faceleft; set => faceleft = value; }

    private void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
