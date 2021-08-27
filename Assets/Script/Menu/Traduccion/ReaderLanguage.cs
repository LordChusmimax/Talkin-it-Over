using System.Collections.Generic;
using UnityEngine;

public class ReaderLanguage
{
    private static readonly Dictionary<string, string> diccionario = new Dictionary<string, string>();
    public static string pruebaTexto;
    private static List<string> listaIdiomas;
    private static SystemLanguage _systemLanguage;

    /// <summary>
    /// Rellenamos el dccionario con el archivo de idioma [Pendiente de solucionar BUG Ascii]
    /// </summary>
    /// <param name="idioma">
    /// 1 = Español
    /// 2 = Ingles
    /// </param>
    public static void loadDiccionary2(int idioma)
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

        diccionario.Clear();
        var file = Resources.Load<TextAsset>("Idiomas/" + _systemLanguage.ToString());
        
        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split('=');
            diccionario[prop[0]] = prop[1];
        }
    }

    public static void loadDiccionary(int idioma)
    {
        if (listaIdiomas == null) { cargarListaIdiomas(); }

        diccionario.Clear();
        var file = Resources.Load<TextAsset>("Idiomas/" + listaIdiomas[idioma]);

        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split('=');
            diccionario[prop[0]] = prop[1].Trim();
        }
    }

    private static void cargarListaIdiomas()
    {
        var files = Resources.LoadAll<TextAsset>("Idiomas");
        listaIdiomas = new List<string>();

        int i = 0;
        foreach (var file in files)
        {
            listaIdiomas.Add(file.name);
            i++;
        }
    }

    /// <summary>
    /// Rellenamos el dccionario con el archivo de idioma
    /// </summary>
    /// <param name="idioma">
    /// 1 = Español
    /// 2 = Ingles
    /// </param>
    public static void loadDiccionary3(int idioma)
    {
        switch (idioma)
        {
            case 0:
                foreach (var line in LanguageSpanish.texto.Split('\n'))
                {
                    var prop = line.Split('=');
                    diccionario[prop[0]] = prop[1];
                }
                break;

            case 1:
                foreach (var line in LanguageEnglish.texto.Split('\n'))
                {
                    var prop = line.Split('=');
                    diccionario[prop[0]] = prop[1];
                }
                break;
        }
    }

    public static Dictionary<string, string> getDictionary()
    {
        return diccionario;
    }

    public static void clearDiccionary()
    {
        diccionario.Clear();
    }

    public static List<string> getIdiomas()
    {
        return listaIdiomas;
    }

    public static string getTextByKey(string key)
    {
        try
        {
            return diccionario[key];
        }
        catch (KeyNotFoundException)
        {
            
            return "No return";
        }
    }

    /// <summary>
    /// Devuelve el tamaño del diccionario para comprobar si se ha rellenado
    /// </summary>
    /// <returns>int = Tamaño del diccionario</returns>
    public static int getDictionaryLenght()
    {
        return diccionario.Count;
    }
}
