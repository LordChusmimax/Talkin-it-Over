using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarTexto : MonoBehaviour
{
    
    /// <summary>
    /// Modifica los textos que haya en pantalla que contengan
    /// el Script de 'SeleccionarTexto'.
    /// </summary>
    /// <param name="idioma">
    /// 0 - Español
    /// 1 - Ingles
    ///  </param>
    public void aplicar(int idioma)
    {
        FindObjectOfType<LectorIdiomas>().aplicarTexto(idioma);
    }
}
