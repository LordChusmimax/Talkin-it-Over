using System;
using System.Linq;
using System.Collections.Generic;

public class PlayerContainer
{

    private static int[] controllers = new int[4];
    private static int numController = 0;

    public static void ayadirControler(int aux)
    {
        controllers[numController] = aux;
        //controllers = controllers.Append(aux);
        numController++;    
    }

    public static void eliminarControler(int aux)
    {
        int numIndex = Array.IndexOf(controllers, aux);
        //controller = controller.Where((val, idx) => idx != numIndex).ToArray();
        controllers = controllers.Except(new int[] { aux }).ToArray();

        numController--;
    }

    public static void limpiarArray()
    {
        numController = 0;
    }

    public static int getController(int aux)
    {
        return controllers[aux];
    }

    public static int[] getArray()
    {
        return controllers;
    }

    public static int getNumController()
    {
        return numController;
    }

}
