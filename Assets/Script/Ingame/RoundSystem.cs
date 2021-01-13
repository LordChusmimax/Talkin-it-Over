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
        
        if (idPlayersLive.Count <= 1)
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
        
    }

    public void setWinner()
    {
        
    }

    public void notWinner()
    {
        StartCoroutine(loadNextRound());
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

    public IEnumerator loadNextRound()
    {
        yield return new WaitForSeconds(5);

        if (finished)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            nextRound();
        }
        
    }

    public void nextRound()
    {
        rondaActual++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
