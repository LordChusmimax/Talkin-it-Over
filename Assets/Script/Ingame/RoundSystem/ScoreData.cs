using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    private static Dictionary<int, int> killsPlayer;
    private static Dictionary<int, int> roundsPlayer;

    /// <summary>
    /// Método donde se comprobarán el número de jugadores antes de empezar la primera
    /// ronda y se prepararán las variables.
    /// </summary>
    public static void initData()
    {
        //Comprobamos si los datos ya están inicializados, en caso de que
        //así sea se saldrá del método.
        if (killsPlayer != null || roundsPlayer != null) { return; }

        //Inicializamos los diccionarios;
        killsPlayer = new Dictionary<int, int>();
        roundsPlayer = new Dictionary<int, int>();

        //Guardamos el número de jugadores de la partida.
        int numPlayers = PlayerContainer.getNumPlayers();

        //Inicalizamos los diccionarios asignado los ids y los valores a 0
        for (int i = 0; i < numPlayers; i++)
        {
            killsPlayer.Add(i, 0);
            roundsPlayer.Add(i, 0);
        }
    }

    /// <summary>
    /// Método donde se le pasará el id del jugador por parámetro y le sumará
    /// en uno su valor en el array.
    /// </summary>
    /// <param name="idPlayer">
    ///     int - Id del jugador.
    /// </param>
    public static void addKill(int idPlayer)
    {
        //Guardamos el número de kills que tiene el jugador actualmente.
        int kills = killsPlayer[idPlayer];

        //Incrementamos en uno su valor y lo guardamos de nuevo en el array.
        killsPlayer[idPlayer] = kills++;
    }

    /// <summary>
    /// Método donde se le pasará el id del jugador por parámetro y le sumará
    /// en uno su valor en el array.
    /// </summary>
    /// <param name="idPlayer">
    ///     int - Id del jugador.
    /// </param>
    public static void addRound(int idPlayer)
    {
        //Guardamos el número de rondas ganada del jugador.
        int rounds = roundsPlayer[idPlayer];

        //Incrementamos en uno su valor y lo guardamos de nuevo en el array.
        roundsPlayer[idPlayer] = rounds++;
    }

    /// <summary>
    /// Método que limipia las puntuaciones de la partida antes de cargar el menú.
    /// </summary>
    public static void clearData()
    {

    }

}
