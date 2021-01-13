using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundSystem : MonoBehaviour
{
    private static RoundSystem current;
    private Scene escenario;
    private int rondaActual = 0;
    private int numPlayerLive;
    private static List<int> idPlayersLive = new List<int>();
    private Dictionary<int, int> playerAndPoint = new Dictionary<int, int>();
    private bool finished = false;
    private Coroutine corrutina;
    public int numRondas = 3;
    
    private void Start()
    {
        noDuplicatedThis();
    }

    private void OnEnable()
    {

        numPlayerLive = PlayerContainer.getNumPlayers();
        idPlayersLive = new List<int>();

        for (int i = 0; i < numPlayerLive; i++)
        {
            idPlayersLive.Add(i);
        }
    }

    public void deletedPlayer(int idPLayer)
    {
        /*for (int i = 0; i < numPlayerLive; i++)
        {
            if (idPlayersLive[i] == idPLayer)
            {
                Debug.Log("Jugador muerto: " + idPlayersLive[i]);
                idPlayersLive.Remove(i);
            }
        }*/
        
        Debug.Log("ID borrado: " + idPlayersLive.Remove(idPLayer));

        if (corrutina == null)
        {
            corrutina = StartCoroutine(readDead());
        }
        else
        {
            StopCoroutine(corrutina);
            corrutina = StartCoroutine(readDead());
        }
    }

    IEnumerator readDead()
    {
        yield return new WaitForSeconds(2);

        if (idPlayersLive.Count == 1)
        {
            foreach (int id in idPlayersLive)
            {

                Debug.Log("ID del jugador vivo es: " + id);

                int aux = playerAndPoint[id];
                aux++;
                //Debug.Log("El juagdor " + id + " ha ganado " + aux + " parridas");
                playerAndPoint[id] = aux;
            }

            foreach (var jug in playerAndPoint)
            {
                Debug.Log("Jugador :" + jug.Key + " tiene " + jug.Value + " victorias");
                if (jug.Value >= numRondas)
                {
                    finished = true;
                }
            }

            StartCoroutine(loadNextRound());

        }
        else if (idPlayersLive.Count < 1)
        {
            Debug.Log(">>>INFO: Nadie ha ganado esta ronda");
            StartCoroutine(loadNextRound());
        }
    }

    IEnumerator loadNextRound()
    {
        yield return new WaitForSeconds(5);
        
        if (finished)
        {
            SceneManager.LoadScene(0);
            StopCoroutine(corrutina);
        }
        else
        {
            nextRound();
        }
    }

    public void nextRound()
    {
        rondaActual++;
        int escena = Random.Range(2, 4);
        SceneManager.LoadScene("Stage" + escena);
        StopCoroutine(corrutina);
    }

    private void noDuplicatedThis()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);

            for (int i = 0; i < numPlayerLive; i++)
            {
                playerAndPoint.Add(i, 0);
            }

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
