using System;
using System.Collections.Generic;

public class PlayerContainer
{

    private static int[] ccccc = new int[4];
    private static Dictionary<int, int> controllers = new Dictionary<int, int>();

    public static void ayadirControler(int aux, int skin)
    {
        controllers.Add(aux, skin);
    }

    public static void limpiarArray()
    {
        controllers.Clear();
    }

    public static int getController(int aux)
    {
        Dictionary<int, int>.KeyCollection keys = controllers.Keys;

        return 0;
    }

    public static Dictionary<int, int> getList()
    {
        return controllers;
    }

    public static int getNumController()
    {
        return controllers.Count;
    }

}
