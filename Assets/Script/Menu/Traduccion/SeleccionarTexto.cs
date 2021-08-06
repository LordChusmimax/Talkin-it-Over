using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SeleccionarTexto : MonoBehaviour
{
    public string id;
    private TextMeshProUGUI componenteTexto;
    
    private void Start()
    {
        modificarTexto();
    }

    public void modificarTexto()
    {
        componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();
        componenteTexto.SetText(ReaderLanguage.getTextByKey(id));
    }

    public void modificarTexto(string extra)
    {
        if (ReaderLanguage.getDictionaryLenght() > 0)
        {
            componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();

            string texto = string.Concat(ReaderLanguage.getTextByKey(id), " " + extra);
            componenteTexto.SetText(texto);
        }
    }


}
