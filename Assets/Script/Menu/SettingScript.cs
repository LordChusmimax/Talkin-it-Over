using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider[] sliders;
    public Toggle tglScreen;

    private void Start()
    {
        loadSettings();
    }

    private void OnDisable()
    {
        saveSettings();
    }

    public void setVolumen(float volumen)
    {
        audioMixer.SetFloat("masterVolumen", volumen);
        //audioMixer.SetFloat("volumen", Mathf.Log10(volumen) * 20);
        Debug.Log(volumen);
    }

    public void setPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    private void saveSettings()
    {

        float masterVolumen = 0;
        int fullScreen = 0;

        audioMixer.GetFloat("masterVolumen", out masterVolumen);
        fullScreen = tglScreen.isOn ? 1 : 0; //¿Es True? -> 1 ¿no lo es? -> 0

        PlayerPrefs.SetFloat("masterVolumen", masterVolumen);
        PlayerPrefs.SetInt("fullScreen", fullScreen);
        PlayerPrefs.Save();

        Debug.Log("INFO: Se han guardado las configuraciones con exito");

    }

    private void loadSettings()
    {
        float masterVolumen = PlayerPrefs.GetFloat("masterVolumen", 0);
        bool fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullScreen", 0));

        setPantallaCompleta(fullScreen);
        tglScreen.isOn = fullScreen;

        Debug.Log(fullScreen);

        audioMixer.SetFloat("masterVolumen", masterVolumen);
        sliders[0].value = masterVolumen;
        
        Debug.Log("INFO: Se ha cargado los datos correctamente");
    }

}
