using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RoundSystem : MonoBehaviour
{
    private static RoundSystem current;
    private Scene escenario;
    private int rondaActual = 0;
    private int numPlayerLive;
    private static List<int> idPlayersLive = new List<int>();
    private static Dictionary<int, int> playerAndPoint = new Dictionary<int, int>();
    private bool finished = false;
    private static bool added = false;
    private Coroutine corrutina;
    public int numRondas = 3;
    public TextMeshProUGUI txtPuntuaciones;
    
    private void Start()
    {
        numPlayerLive = PlayerContainer.getNumPlayers();
        idPlayersLive = new List<int>();
        
        prepareData();
    }

    private void OnDestroy()
    {
        if (corrutina != null)
        {
            StopCoroutine(corrutina);
        }
    }

    public void deletedPlayer(int idPLayer)
    {        
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

            string iniciarTexto = "";

            foreach (var jug in playerAndPoint)
            {
                //Debug.Log("Jugador :" + jug.Key + " tiene " + jug.Value + " victorias");

                iniciarTexto += ReaderLanguage.getTextByKey("puntuacion1") + (jug.Key + 1) + " - " + jug.Value + "/" + numRondas + ReaderLanguage.getTextByKey("puntuacion2") + "\n";

                txtPuntuaciones.SetText(iniciarTexto);

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
            limpiarResultados();
            SceneManager.LoadScene(0);
            StopCoroutine(corrutina);
        }
        else
        {
            nextRound();
        }
    }

    public static void limpiarResultados()
    {
        current = null;
        idPlayersLive.Clear();
        playerAndPoint.Clear();
        added = false;
    }

    public void nextRound()
    {
        rondaActual++;
        int escena = Random.Range(1, 6);
        SceneManager.LoadScene("Stage" + escena);
        StopCoroutine(corrutina);
    }

    private void prepareData()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);

            if (!added)
            {
                for (int i = 0; i < numPlayerLive; i++)
                {
                    playerAndPoint.Add(i, 0);
                }

                string iniciarTexto = "";

                for (int i = 0; i < numPlayerLive; i++)
                {
                    idPlayersLive.Add(i);
                    //iniciarTexto += "Jugador " + (i+ 1) + " - 0 / " + numRondas + " ganadas\n";
                    iniciarTexto += ReaderLanguage.getTextByKey("puntuacion1") + (i + 1) + " - 0/" + numRondas + ReaderLanguage.getTextByKey("puntuacion2") + "\n";
                    //iniciarTexto += ReaderLanguage.getTextByKey("puntuacion1");
                }

                txtPuntuaciones.SetText(iniciarTexto);

                added = true;
            }
            else
            {
                string iniciarTexto = "";

                for (int i = 0; i < numPlayerLive; i++)
                {
                    idPlayersLive.Add(i);
                }

                foreach (var jug in playerAndPoint)
                {
                    iniciarTexto += ReaderLanguage.getTextByKey("puntuacion1") + (jug.Key + 1) + " - " + jug.Value + "/" + numRondas + ReaderLanguage.getTextByKey("puntuacion2") + "\n";
                }

                txtPuntuaciones.SetText(iniciarTexto);

            }

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
