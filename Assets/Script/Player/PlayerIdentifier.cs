using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    private PlayerScript[] players;
    private Transform[] spawners;
    private GameObject spawnerCollector;

    void Start()
    {
        var i = 0;
        players = GetComponentsInChildren<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.SetLayer(i++);
        }

        spawnerCollector = GetComponentInChildren<SpawnerScript>().gameObject;
        spawners = spawnerCollector.GetComponentsInChildren<Transform>();

        List<int> randomOrder = randomNumberList();

        int index = 0;
        foreach (PlayerScript player in players)
        {
            player.transform.position = spawners[randomOrder[index]].transform.position;
            index++;
        }


    }

    private List<int> randomNumberList()
    {
        List<int> randomOrder = new List<int>();
        randomOrder.Add(1);
        randomOrder.Add(2);
        randomOrder.Add(3);
        randomOrder.Add(4);
        randomOrder = randomOrder.OrderBy(j => Guid.NewGuid()).ToList();
        return randomOrder;
    }

}
