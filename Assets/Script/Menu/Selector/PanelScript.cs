using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] skin;

    /// <summary>
    /// Funci�n llamada desde el script 'PlayerSelectorScript' y que activar� el panel correspondiente.
    /// </summary>
    /// <param name="deviceArrayPosition">Panel a activar</param>
    public void addPlayereInPanel(int deviceArrayPosition)
    {
        //Debug.Log("INFO: A�adiendo controlador en la posici�n: " + deviceArrayPosition);
        isPanelActive(true, deviceArrayPosition);
    }

    /// <summary>
    /// Funci�n llamada desde el script 'PlayerSelectorScript' y que desactivar� el panel correspondiente.
    /// </summary>
    /// <param name="deviceArrayPosition">Panel a desactivar</param>
    public void removePlayerinPanel(int deviceArrayPosition)
    {
        //Debug.Log("INFO: Eliminando controlador en la posici�n: " + deviceArrayPosition);
        isPanelActive(false, deviceArrayPosition);
    }


    /// <summary>
    /// Funci�n al que se le indicar� si tiene que activar o desactivar un panel
    /// y cual deber� modificar
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
