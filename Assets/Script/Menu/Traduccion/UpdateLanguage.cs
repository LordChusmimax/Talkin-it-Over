using UnityEngine;
using TMPro;

public class UpdateLanguage : MonoBehaviour
{
    
    public void modificarIdioma(int idioma)
    {
        ReaderLanguage.loadDiccionary(idioma);

        var objetos = Resources.FindObjectsOfTypeAll<SeleccionarTexto>();

        foreach (var texto in objetos)
        {
            var componenteTexto = texto.GetComponent<TextMeshProUGUI>();
            componenteTexto.text = ReaderLanguage.getWordByKey(texto.id);

        }

    }

}
