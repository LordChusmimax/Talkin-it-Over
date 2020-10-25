using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAssigner : MonoBehaviour
{
    public static ControllerAssigner current;
    protected PlayerScript[] players;
    public GameObject prefabPlayer;
    [SerializeField]protected int index = -1;
    void Start()
    {
        current = this;

        int num = PlayerContainer.getNumController();

        for (int i = 0; i < num; i++)
        {
            PlayerScript player = Instantiate(prefabPlayer, transform).GetComponent<PlayerScript>();
            player.SelectController(PlayerContainer.getController(i));
        }
    }

}
