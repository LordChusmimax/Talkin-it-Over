using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAssigner : MonoBehaviour
{
    public static ControllerAssigner current;
    private PlayerScript[] players;
    public GameObject prefab;
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

        PlayerScript newPlayer = Instantiate(prefab, transform).GetComponent<PlayerScript>();
        newPlayer.SelectController(0);
    }

}
