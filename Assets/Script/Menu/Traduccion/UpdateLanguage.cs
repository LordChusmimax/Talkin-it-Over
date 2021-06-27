using UnityEngine;
using TMPro;
using System;

public class UpdateLanguage : MonoBehaviour
{
    private int idioma;

    public void modificarIdioma(int intIdioma)
    {
        ReaderLanguage.clearDiccionary();
        ReaderLanguage.loadDiccionary(intIdioma);

        this.idioma = intIdioma;
        var objetos = Resources.FindObjectsOfTypeAll<SeleccionarTexto>();

        foreach (var texto in objetos)
        {
            var componenteTexto = texto.GetComponent<TextMeshProUGUI>();
            componenteTexto.SetText(ReaderLanguage.getTextByKey(texto.id).ToString());
        }


        //Debug.Log(ReaderLanguage.getTextByKey("volviendo") + " " + 10);
    }

    public void setIdioma(int idioma)
    {
        if (idioma < 0)
        {
            this.idioma = 0;
            return;
        }
        this.idioma = idioma;
    }

    public int getIdioma()
    {
        return this.idioma;
    }
}
