using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraducirBoton : MonoBehaviour
{

    public string clave;

    void Start()
    {
        this.GetComponent<Text>().text = "" + LecturaArchivo.Field[clave];
    }

    public void modificarTexto()
    {
        this.GetComponent<Text>().text = "" + LecturaArchivo.Field[clave];
    }

}
