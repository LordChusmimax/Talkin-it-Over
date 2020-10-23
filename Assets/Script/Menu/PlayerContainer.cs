using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer
{

    private static int[] controller = new int[4];
    private static int numController = 0;
    public static int getNumController = 0;

    public static void ayadirControler(int aux)
    {
        controller[numController] = aux;
        numController++;
        getNumController++;
    }

    public static int getController(int aux)
    {
        return controller[aux];
    }

}
