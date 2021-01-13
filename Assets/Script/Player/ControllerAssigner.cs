using System.Collections.Generic;
using UnityEngine;


public class ControllerAssigner : MonoBehaviour
{
    public static ControllerAssigner current;
    protected PlayerScript[] players;
    public GameObject[] prefabPlayers;
    [SerializeField]protected int index = -1;
    void Start()
    {
        current = this;

        Dictionary<int, int> lista = PlayerContainer.getList();
        int i = 0;

        foreach (KeyValuePair<int, int> elemento in lista)
        {
            PlayerScript player = Instantiate(prefabPlayers[elemento.Value], transform).GetComponent<PlayerScript>();
            player.SelectController(elemento.Key);
        }
    }

}
