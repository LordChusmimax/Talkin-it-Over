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

        Dictionary<int, int> lista = PlayerContainer.getList();
        int i = 0;

        foreach (KeyValuePair<int, int> elemento in lista)
        {
            PlayerScript player = Instantiate(prefabPlayer, transform).GetComponent<PlayerScript>();
            player.SelectController(elemento.Key);
        }
    }

}
