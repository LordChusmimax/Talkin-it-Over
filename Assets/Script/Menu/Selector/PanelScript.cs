using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] skin;

    /// <summary>
    /// Función llamada desde el script 'PlayerSelectorScript' y que activará el panel correspondiente.
    /// </summary>
    /// <param name="deviceArrayPosition">Panel a activar</param>
    public void addPlayereInPanel(int deviceArrayPosition)
    {
        //Debug.Log("INFO: Añadiendo controlador en la posición: " + deviceArrayPosition);
        isPanelActive(true, deviceArrayPosition);
    }

    /// <summary>
    /// Función llamada desde el script 'PlayerSelectorScript' y que desactivará el panel correspondiente.
    /// </summary>
    /// <param name="deviceArrayPosition">Panel a desactivar</param>
    public void removePlayerinPanel(int deviceArrayPosition)
    {
        //Debug.Log("INFO: Eliminando controlador en la posición: " + deviceArrayPosition);
        isPanelActive(false, deviceArrayPosition);
    }


    /// <summary>
    /// Función al que se le indicará si tiene que activar o desactivar un panel
    /// y cual deberá modificar
    /// </summary>
    /// <param name="active">
    /// True -> Activar panel
    /// False -> Desactivar panel
    /// </param>
    /// <param name="panelPosition">Panel a modificar</param>
    private void isPanelActive(bool active, int panelPosition)
    {
        //Guardamos el objeto panel que vamos a modificar
        Transform miPanel = transform.GetChild(panelPosition);

        //Modificamos el estado del objeto panel
        GameObject aux = miPanel.gameObject;
        aux.SetActive(active);
    }
}
