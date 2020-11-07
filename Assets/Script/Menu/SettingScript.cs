using UnityEngine;
using UnityEngine.Audio;

public class SettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void setVolumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen - 40);
        //audioMixer.SetFloat("volumen", Mathf.Log10(volumen) * 20);
        Debug.Log(volumen);
    }

    public void setPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

}
