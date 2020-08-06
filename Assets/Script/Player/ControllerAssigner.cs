using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssigner : MonoBehaviour
{
    public static ControllerAssigner current;
    private PlayerScript[] players;
    [SerializeField] int index = -1;
    void Start()
    {
        current = this;
        players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SelectController(index);
            index++;
        }
    }

}
