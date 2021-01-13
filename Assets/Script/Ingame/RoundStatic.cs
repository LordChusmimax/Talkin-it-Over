using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStatic
{
    private static List<int> jugadoresVivos = new List<int>();
    private static Dictionary<int, int> playerAndPoint = new Dictionary<int, int>();
    private static int jugadores;

    public static void iniciarPartida(int numJugadores)
    {
        jugadores = numJugadores;

        for (int i = 0; i < numJugadores; i++)
        {
            playerAndPoint.Add(i, 0);
        }
    }



}
