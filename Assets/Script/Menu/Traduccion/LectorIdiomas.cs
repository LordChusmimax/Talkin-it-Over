using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LectorIdiomas : MonoBehaviour
{
    //Creamos el diccionario donde se almacenará los datos
    private readonly Dictionary<string, string> _lang = new Dictionary<string, string>();

    //Analizamos en que idioma se encuentra el sistema del usuario
    private SystemLanguage _systemLanguage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        leerPropiedades();
        
    }


    /// <summary>
    /// Asignamos el idioma y guardamos la información del texto
    /// dentro del diccionario
    /// </summary>
    private void leerPropiedades()
    {
        _systemLanguage = Application.systemLanguage;

        //Comprobamos que el sistema donde se ejecuta tiene soporte en algún idioma, de no ser así se le dará uno por defecto
        //var file = Resources.Load<TextAsset>("Idiomas/" + _systemLanguage.ToString());
        var file = Resources.Load<TextAsset>("Idiomas/English");

        if (file == null)
        {
            //En caso de no tener un sistema, se le asignará el ingles por defecto
            _systemLanguage = SystemLanguage.English;
            file = Resources.Load<TextAsset>(_systemLanguage.ToString());
        }
        
        
        //Separar en las diferentes líneas que hay dentro del archivo de texto
        foreach (var line in file.text.Split('\n'))
        {
            //Separas cada línea de texto en 2 partes separadas por el signo '=' y als asignas al diccionario
            var prop = line.Split('=');
            _lang[prop[0]] = prop[1];
        }
        Debug.Log("Se ha leido el archivo");

    }

    public void aplicarTexto(int idioma)
    {
        
        switch (idioma)
        {
            case 0:
                _systemLanguage = SystemLanguage.Spanish;
                break;

            case 1:
                _systemLanguage = SystemLanguage.English;
                break;
        }

        _lang.Clear();
        var file = Resources.Load<TextAsset>("Idiomas/" + _systemLanguage.ToString());
        
        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split('=');
            _lang[prop[0]] = prop[1];
        }


        var objetos = Resources.FindObjectsOfTypeAll<SeleccionarTexto>();

        foreach (var texto in objetos)
        {
            var componenteTexto = texto.GetComponent<TextMeshProUGUI>();
            componenteTexto.text = _lang[texto.id];

        }
        
    }

}
