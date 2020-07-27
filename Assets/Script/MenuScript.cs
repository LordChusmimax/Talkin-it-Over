using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("Hola Mundo");
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Lab");
    }

    public void Salir()
    {

        Debug.Log("Salir");
        Application.Quit();
    }

}
