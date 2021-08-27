using UnityEngine;
using TMPro;

public class SeleccionarTexto : MonoBehaviour
{
    public string id;
    private TextMeshProUGUI componenteTexto;
    
    private void OnEnable()
    {
        modificarTexto();
    }

    public void modificarTexto()
    {
        
        if (ReaderLanguage.getDictionaryLenght() > 0)
        {
            componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();
            componenteTexto.SetText(ReaderLanguage.getTextByKey(id));
        }
    }

    public void modificarTexto(string extra)
    {
        if (ReaderLanguage.getDictionaryLenght() > 0)
        {
            componenteTexto = gameObject.GetComponent<TextMeshProUGUI>();
            componenteTexto.SetText(ReaderLanguage.getTextByKey(id) + " " + extra);
        }
    }


}
