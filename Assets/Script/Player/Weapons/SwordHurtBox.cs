using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHurtBox : MonoBehaviour
{
    private bool faceleft;
    private int idPlayer;
    public bool hited = false;

    public bool Faceleft { get => faceleft; set => faceleft = value; }
    public int IdPlayer { get => idPlayer; set => idPlayer = value; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && !hited)
        {
            hited = true;
            ScoreData.addKill(idPlayer);
        }
    }
}
