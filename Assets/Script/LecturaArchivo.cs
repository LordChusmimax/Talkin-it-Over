using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LecturaArchivo : MonoBehaviour
{
    public string archivo = "es";
    public TraducirBoton botonScript;
    private List<string> lineas;
    private string clave;
    private string contenido;

    public static Dictionary<string, string> Field { get; private set; }

    void Start()
    {
        leerArchivo(archivo);
    }

    public void leerArchivo(string archivo)
    {
        string ruta = "Assets/Script/Lenguajes/" + archivo + ".txt";
        if (Field == null)
        {
            Field = new Dictionary<string, string>();
        }
        Field.Clear();

        StreamReader lector = new StreamReader(ruta);

        lineas = new List<string>();
        lineas.AddRange(lector.ReadToEnd().Split("\n"[0]));

        for (int i = 0; i < lineas.ToArray().Length; i++)
        {
            clave = lineas[i].Substring(0, lineas[i].IndexOf("="));
            contenido = lineas[i].Substring(lineas[i].IndexOf("=") + 1, lineas[i].Length - lineas[i].IndexOf("=") - 1);

            Field.Add(clave, contenido);
        }

        lector.Close();
        botonScript.modificarTexto();
        Debug.Log("Traducción realizada con éxito");
    }
}
