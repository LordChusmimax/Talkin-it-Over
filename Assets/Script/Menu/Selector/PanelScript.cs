using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] skins;
    private bool[] skinsUsed;

    private void Awake()
    {
        //Creamos una lista donde indicaremos las sins que estarán usadas
        //por otros  jugadores
        skinsUsed = new bool[skins.Length];



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
    public void activePanel(bool active, int panelPosition)
    {
        //Guardamos el objeto panel que vamos a modificar
        Transform myPanel = transform.GetChild(panelPosition);

        //Modificamos el estado del objeto panel
        GameObject aux = myPanel.gameObject;
        aux.SetActive(active);


    }


    public void changeSkin()
    {

    }
}
