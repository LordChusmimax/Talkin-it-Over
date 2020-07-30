using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssigner : MonoBehaviour
{
    PlayerScript[] players;
    [SerializeField] int indice = -1;
    void Start()
    {
        players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SelectController(indice);
            indice++;
        }
        var i = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "Player")
            {
                child.position = transform.Find("SpawnPoints").GetChild(i).position;
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
