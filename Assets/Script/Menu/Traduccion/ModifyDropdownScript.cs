using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyDropdownScript : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropdown;

    private void Awake()
    {
        fillDropdown();
    }

    /// <summary>
    /// Rellenaremos el Dropdown con la cantidad de
    /// archivos de idimas detectados en el directorio
    /// </summary>
    private void fillDropdown()
    {
        List<string> idiomas = ReaderLanguage.getIdiomas();
        dropdown.AddOptions(idiomas);
    }
}
