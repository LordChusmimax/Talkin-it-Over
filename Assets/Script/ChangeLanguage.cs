using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class ChangeLanguage:MonoBehaviour
{
    public Dropdown dropdown;
    public TextMeshProUGUI textMesh;
    private void Start()
    {
        
    }

    public void DropdownValueChanged()
    {
        Language = dropdown.value;
        textMesh.text = MenuTexts.gameTitle;
    }
}
