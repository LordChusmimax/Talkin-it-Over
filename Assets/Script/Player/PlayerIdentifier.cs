using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    private PlayerScript[] players;

    void Start()
    {
        var i = 0;
        players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SetIndex(i++);
        }
    }

}
