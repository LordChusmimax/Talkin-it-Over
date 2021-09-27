using System.Collections;
using System.Collections.Generic;

public class PlayerContainer
{
    private static Dictionary<int, int> controllers = new Dictionary<int, int>();

    /// <summary>
    /// Método donde se guarda el controlador del jugador y la skin asociada
    /// a él.
    /// </summary>
    /// <param name="aux">
    ///     int - Controlador del jugador.
    /// </param>
    /// <param name="skin">
    ///     int - Skin asignada al controlador.
    /// </param>
    public static void addController(int aux, int skin)
    {
        controllers.Add(aux, skin);
    }

    /// <summary>
    /// Método que limpia los elementos del array
    /// </summary>
    public static void clearArray()
    {
        controllers.Clear();
    }

    /// <summary>
    /// Método que devuelve el diccionario donde se guardan los jugadores y
    /// controladores asignados.
    /// </summary>
    /// <returns>
    ///     Dictionary<int, int> - Diccionario con los jugadores y skins asignados
    /// </returns>
    public static Dictionary<int, int> getList()
    {
        return controllers;
    }

    /// <summary>
    /// Método que devuelve el número de jugadores dentro del array
    /// </summary>
    /// <returns>
    ///     int - Número de jugadores
    /// </returns>
    public static int getNumPlayers()
    {
        return controllers.Count;
    }
    
}
