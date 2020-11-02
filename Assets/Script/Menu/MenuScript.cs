using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    public GameObject menuFirstButton;
    public GameObject optionsFirstButton;
    public GameObject optionsCloseButton;

    /// <summary>
    /// Método llamado desde el selector de jugadores
    /// </summary>
    public void Jugar()
    {
        SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// Método con el que se inicia una partida personalizada para testear
    /// </summary>
    public void Lab()
    {
        SceneManager.LoadScene("Lab");
    }

    /// <summary>
    /// Métodos con los que modificamos el botón seleccionado al cambiar de panel
    /// </summary>
    public void abrirOpciones()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    public void cerrarOpciones()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }
    public void cerrarSelector()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

}
