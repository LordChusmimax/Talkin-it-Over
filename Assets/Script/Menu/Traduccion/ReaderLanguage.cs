using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReaderLanguage
{
    private static readonly Dictionary<string, string> diccionario = new Dictionary<string, string>();
    public static string pruebaTexto;
    private static SystemLanguage _systemLanguage;

    /// <summary>
    /// Rellenamos el dccionario con el archivo de idioma
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

    /// <summary>
    /// Rellenamos el dccionario con el archivo de idioma
    /// </summary>
    /// <param name="idioma">
    /// 1 = Español
    /// 2 = Ingles
    /// </param>
    public static void loadDiccionary(int idioma)
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

    public static string getTextByKey(string key)
    {
        return diccionario[key];
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
