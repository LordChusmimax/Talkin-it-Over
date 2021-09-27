using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    private static Dictionary<int, int> killsPlayer;
    private static Dictionary<int, int> roundsPlayer;

    /// <summary>
    /// M�todo donde se comprobar�n el n�mero de jugadores antes de empezar la primera
    /// ronda y se preparar�n las variables.
    /// </summary>
    public static void initData()
    {
        //Comprobamos si los datos ya est�n inicializados, en caso de que
        //as� sea se saldr� del m�todo.
        if (killsPlayer != null || roundsPlayer != null) { return; }

        //Inicializamos los diccionarios;
        killsPlayer = new Dictionary<int, int>();
        roundsPlayer = new Dictionary<int, int>();

        //Guardamos el n�mero de jugadores de la partida.
        int numPlayers = PlayerContainer.getNumPlayers();

        //Inicalizamos los diccionarios asignado los ids y los valores a 0
        for (int i = 0; i < numPlayers; i++)
        {
            killsPlayer.Add(i, 0);
            roundsPlayer.Add(i, 0);
        }
    }

    /// <summary>
    /// M�todo donde se le pasar� el id del jugador por par�metro y le sumar�
    /// en uno su valor en el array.
    /// </summary>
    /// <param name="idPlayer">
    ///     int - Id del jugador.
    /// </param>
    public static void addKill(int idPlayer)
    {
        //Guardamos el n�mero de kills que tiene el jugador actualmente.
        int kills = killsPlayer[idPlayer];

        //Incrementamos en uno su valor y lo guardamos de nuevo en el array.
        killsPlayer[idPlayer] = kills++;
    }

    /// <summary>
    /// M�todo donde se le pasar� el id del jugador por par�metro y le sumar�
    /// en uno su valor en el array.
    /// </summary>
    /// <param name="idPlayer">
    ///     int - Id del jugador.
    /// </param>
    public static void addRound(int idPlayer)
    {
        //Guardamos el n�mero de rondas ganada del jugador.
        int rounds = roundsPlayer[idPlayer];

        //Incrementamos en uno su valor y lo guardamos de nuevo en el array.
        roundsPlayer[idPlayer] = rounds++;
    }

    /// <summary>
    /// M�todo que limipia las puntuaciones de la partida antes de cargar el men�.
    /// </summary>
    public static void clearData()
    {

    }

}
