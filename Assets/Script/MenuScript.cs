using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    private void Start()
    {
        
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Lab");
    }

    public void pruebas()
    {
        Debug.Log(PruebaLectura.Field["jugar"]);
    }

    public void Salir()
    {

        Debug.Log("Salir");
        Application.Quit();
    }

}
