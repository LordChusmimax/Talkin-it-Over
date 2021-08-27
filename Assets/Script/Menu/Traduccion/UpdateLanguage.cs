using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UpdateLanguage : MonoBehaviour
{
    private int idioma = 0;
    private bool selected = false;

    [SerializeField]
    private Dropdown dropdown;

    private void OnEnable()
    {
        if (!selected)
        {
            selectValue();
        }
    }

    /// <summary>
    /// Modificamos el diccionario del idioma y actualizamos los
    /// componentes que se ecuentrar en la memoria
    /// </summary>
    /// <param name="idioma">
    /// Entero devuelto por el Dropdown del idioma
    /// </param>
    public void modificarIdioma(int idioma)
    {
        ReaderLanguage.clearDiccionary();
        ReaderLanguage.loadDiccionary(idioma);

        this.idioma = idioma;
        var objetos = Resources.FindObjectsOfTypeAll<SeleccionarTexto>();

        foreach (var texto in objetos)
        {
            var componenteTexto = texto.GetComponent<TextMeshProUGUI>();
            componenteTexto.SetText(ReaderLanguage.getTextByKey(texto.id).ToString());
        }
    }

    public void selectValue()
    {
        dropdown.value = idioma;
        selected = true;
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
