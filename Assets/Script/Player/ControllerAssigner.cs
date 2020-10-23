using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAssigner : MonoBehaviour
{
    public static ControllerAssigner current;
    private PlayerScript[] players;
    public GameObject prefabPlayer;
    [SerializeField] int index = -1;
    void Start()
    {
        current = this;
        /*players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SelectController(index);
            index++;
        }*/

        int num = PlayerContainer.getNumController;

        for (int i = 0; i < num; i++)
        {
            PlayerScript player = Instantiate(prefabPlayer, transform).GetComponent<PlayerScript>();
            player.SelectController(PlayerContainer.getController(i));
        }
    }

}
