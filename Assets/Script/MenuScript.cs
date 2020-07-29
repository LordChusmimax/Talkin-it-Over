using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public PruebaLectura prueba;

    public void Jugar()
    {
        SceneManager.LoadScene("Lab");
    }

    public void pruebas()
    {

        modificarIdiomaEspanol();
    }

    public void pruebas2()
    {

        //modificarIdiomaIngles();
        var children = GameObject.Find("btnJugar").GetComponentsInChildren<Transform>();
        Debug.Log(children[0].name);
    }

    public void Salir()
    {

        Debug.Log("Salir");
        Application.Quit();
    }

    void modificarIdiomaEspanol()
    {
        prueba.leerArchivo("es");
        GameObject.Find("btnJugar").GetComponentInChildren<Text>().text = PruebaLectura.Field["jugar"];

        Debug.Log("Se ha modificado el texto");
    }

    void modificarIdiomaIngles()
    {
        prueba.leerArchivo("en");
        GameObject.Find("btnJugar").GetComponentInChildren<Text>().text = PruebaLectura.Field["jugar"];
        
        Debug.Log("Se ha modificado el texto");
    }

}
