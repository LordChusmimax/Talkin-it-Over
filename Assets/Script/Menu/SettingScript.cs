using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider[] sliders;
    public Toggle tglScreen;
    public Dropdown drpIdioma;

    private void Start()
    {
        loadSettings();
    }

    private void OnDisable()
    {
        saveSettings();
    }

    public void setMusica(float volumen)
    {
        audioMixer.SetFloat("musicVolumen", volumen);
        //audioMixer.SetFloat("volumen", Mathf.Log10(volumen) * 20);
        Debug.Log(volumen);
    }

    public void setEfectos(float volumen)
    {
        audioMixer.SetFloat("efectVolumen", volumen);
        //audioMixer.SetFloat("volumen", Mathf.Log10(volumen) * 20);
        Debug.Log(volumen);
    }

    public void setPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

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

        Debug.Log("INFO: Se han guardado las configuraciones con exito");

    }

    private void loadSettings()
    {
        float musicVolumen = PlayerPrefs.GetFloat("musicVolumen", 0);
        float efectVolumen = PlayerPrefs.GetFloat("efectVolumen", 0);
        bool fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullScreen", 0));
        int idioma = PlayerPrefs.GetInt("drpIdioma", 0);

        setPantallaCompleta(fullScreen);
        tglScreen.isOn = fullScreen;

        //Debug.Log(fullScreen);

        audioMixer.SetFloat("musicVolumen", musicVolumen);
        audioMixer.SetFloat("efectVolumen", efectVolumen);
        sliders[0].value = musicVolumen;
        sliders[1].value = efectVolumen;
        drpIdioma.value = idioma;

        Debug.Log("INFO: Se ha cargado los datos correctamente");
    }

}
