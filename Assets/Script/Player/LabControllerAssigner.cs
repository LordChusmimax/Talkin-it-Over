using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabControllerAssigner : ControllerAssigner
{
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
