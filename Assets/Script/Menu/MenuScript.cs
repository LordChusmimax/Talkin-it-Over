using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// Esto es un comentario
    /// </summary>

    public void Jugar()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Lab()
    {
        SceneManager.LoadScene("Lab");
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

}
