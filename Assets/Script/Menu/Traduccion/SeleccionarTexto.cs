using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class SeleccionarTexto : MonoBehaviour
{
    public string id;
    private TextMeshProUGUI componenteTexto;
    
    private void Start()
    {
        modificarTexto();
    }

    /// <summary>
    /// Métoddo que modifica el texto del componente 'TextMeshPro' del propio
    /// objeto al que pertenece, leyendo el diccionario del Script 'ReaderLanguage'.
    /// </summary>
    public void modificarTexto()
    {
        componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();
        componenteTexto.SetText(ReaderLanguage.getTextByKey(id));
    }

    /// <summary>
    /// Método que modifia el texto del componente 'TextMeshPro' del propio
    /// objeto y que, a diferencia del anterior, añade un valor extra facilitado
    /// desde otro Script.
    /// </summary>
    /// <param name="extra">
    ///     string - Cadena que se añadirá al texto sustituyendo la palabra clave.
    /// </param>
    public void modificarTexto(params string[] extra)
    {
        if (ReaderLanguage.getDictionaryLenght() > 0)
        {
            //Guardamos el componente para su posterior modificación
            componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();

            //Guardamos el texto ya modificado por el método 'concatText' tras leerlo del archivo
            //de idiomas.
            string text = concatText(ReaderLanguage.getTextByKey(id), extra);

            //Modificamos el texto del componente para que muestre el nuevo mensaje
            componenteTexto.SetText(text);
        }
    }


    /// <summary>
    /// Método donde se le facilita un texto, y se les añade el elemento facilitado en
    /// el segundo parámetro en la zona marcada con <name> dentro del 'string'. 
    /// </summary>
    /// <param name="textRaw">
    ///     string - Texto en bruto donde se buscará las palabras clave
    /// </param>
    /// <param name="replacer">
    ///     string - Elementos a añadir dentro de las palabras clave del texto
    /// </param>
    /// <returns>
    ///     string - Texto con las modificaciones realizadas
    /// </returns>
    private string concatText(string textRaw, string[] replacer)
    {
        //Creamos la variable donde guardaremos el texto que devolverá el método y
        //otra variable donde inicializaremos el contador de 'replacer' para el array
        string textModify = "";
        int numReplacer = 0;

        //Guardamos cada palabra en un array
        string[] words = textRaw.Split(' ');


        //Recorremos el array donde se guardan las palabras
        foreach (string word in words)
        {

            //Comprobamos si la palabra obtenida coincide con la palabra clave
            if (word.Equals("<data>"))
            {
                try
                {
                    //Cambiamos la palabra por la facilitada en el parámetro 
                    textModify += replacer[numReplacer] + " ";
                }
                catch (IndexOutOfRangeException exception)
                {
                    textModify += "ERROR";
                    Debug.Log("ERROR: Número de elementos recividos insuficientes\n" + exception);
                }
                //Sumamos uno el valor de 'numReplacer'
                numReplacer++;

                //Pasamos al siguiente elemento del array
                continue;
            }

            //Si no coincide con la palabra clave se asignará sin modifcar
            textModify += word + " ";
        }

        //Devolvemos el texto
        return textModify;
    }

}
