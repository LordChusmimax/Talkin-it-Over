using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider[] sliders;
    public Toggle tglScreen;
    public Dropdown drpIdioma;

    /// <summary>
    /// Al iniciar leemos los datos almacenados en el PlayerPref o toma
    /// los datos por defecto
    /// </summary>
    private void Awake()
    {
        loadSettings();
    }

    /// <summary>
    /// Guardamos los datos de configuración al desactivar el controlador
    /// de las opciones
    /// </summary>
    private void OnDisable()
    {
        saveSettings();
    }

    public void setMusica(float volumen)
    {
        audioMixer.SetFloat("musicVolumen", volumen);
    }

    public void setEfectos(float volumen)
    {
        audioMixer.SetFloat("efectVolumen", volumen);
        Debug.Log(volumen);
    }

    public void setPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }


    /// <summary>
    /// Tomamos los datos de  configuración y los 
    /// guardamos en el PlayerPref
    /// </summary>
    private void saveSettings()
    {

        float musicVolumen = 0;
        float efectVolumen = 0;
        int fullScreen = 0;
        int idioma = 0;

        audioMixer.GetFloat("musicVolumen", out musicVolumen);
        audioMixer.GetFloat("efectVolumen", out efectVolumen);
        fullScreen = tglScreen.isOn ? 1 : 0; //¿Es True? -> 1 ¿no lo es? -> 0
        idioma = drpIdioma.value;

        PlayerPrefs.SetFloat("musicVolumen", musicVolumen);
        PlayerPrefs.SetFloat("efectVolumen", efectVolumen);
        PlayerPrefs.SetInt("fullScreen", fullScreen);
        PlayerPrefs.SetInt("drpIdioma", idioma);
        PlayerPrefs.Save();

        //Debug.Log("INFO: Se han guardado las configuraciones con exito");

    }

    /// <summary>
    /// Cargamos los datos guardados en el PlayerPrefs
    /// o usamos los datos por defeecto
    /// </summary>
    private void loadSettings()
    {
        float musicVolumen = PlayerPrefs.GetFloat("musicVolumen", 0);
        float efectVolumen = PlayerPrefs.GetFloat("efectVolumen", 0);
        bool fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullScreen", 0));
        int idioma = PlayerPrefs.GetInt("drpIdioma", 0);

        setPantallaCompleta(fullScreen);
        tglScreen.isOn = fullScreen;

        audioMixer.SetFloat("musicVolumen", musicVolumen);
        audioMixer.SetFloat("efectVolumen", efectVolumen);
        sliders[0].value = musicVolumen;
        sliders[1].value = efectVolumen;
        ReaderLanguage.loadDiccionary(idioma);
        //drpIdioma.value = idioma;
        drpIdioma.GetComponent<UpdateLanguage>().setIdioma(idioma);
        //Debug.Log("INFO: Se ha cargado los datos correctamente");
    }

}
