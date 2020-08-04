using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarTexto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<LectorIdiomas>().aplicarTexto();
    }

    public void aplicar(int idioma)
    {
        FindObjectOfType<LectorIdiomas>().aplicarTexto(idioma);
    }
}
