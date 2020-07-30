using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssigner : MonoBehaviour
{
    PlayerScript[] players;
    [SerializeField] int index = -1;
    void Start()
    {
        players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SelectController(index);
            index++;
        }
    }

}
