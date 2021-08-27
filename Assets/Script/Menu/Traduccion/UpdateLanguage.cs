using UnityEngine;
using TMPro;

public class UpdateLanguage : MonoBehaviour
{
    public void modificarIdioma(int idioma)
    {
        ReaderLanguage.clearDiccionary();
        ReaderLanguage.loadDiccionary(idioma);

        var objetos = Resources.FindObjectsOfTypeAll<SeleccionarTexto>();

        foreach (var texto in objetos)
        {
            var componenteTexto = texto.GetComponent<TextMeshProUGUI>();
            componenteTexto.SetText(ReaderLanguage.getTextByKey(texto.id).ToString());
        }

    }

}
