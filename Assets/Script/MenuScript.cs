using System.Collections;
using System;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("Hola Mundo");
    }

    public void Jugar()
    {

    }

    public void Salir()
    {

        Debug.Log("Salir");
        Application.Quit();
    }

}
