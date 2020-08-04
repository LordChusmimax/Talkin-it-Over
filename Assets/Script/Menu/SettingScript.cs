using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void setVolumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void setPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

}
