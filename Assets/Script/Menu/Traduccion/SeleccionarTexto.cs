using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeleccionarTexto : MonoBehaviour
{
    public string id;

    private void OnEnable()
    {
        modificarTexto();
    }

    public void modificarTexto()
    {
        if (ReaderLanguage.getDictionaryLenght() > 0)
        {
            var componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();
            componenteTexto.text = ReaderLanguage.getWordByKey(id);
        }
    }

}
