using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptModifyPanel : MonoBehaviour
{

    private int numJugadores = 0;

    public void ayadirJugador()
    {
        transform.GetChild(numJugadores).GetComponent<Image>().color = Color.red;
        numJugadores++;
    }
}
