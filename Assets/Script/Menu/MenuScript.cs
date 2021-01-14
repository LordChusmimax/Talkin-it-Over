using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    public GameObject menuFirstButton;
    public GameObject optionsFirstButton;
    public GameObject optionsCloseButton;
    public GameObject helpFirstButton;
    public GameObject helpCloseButton;

    private void Awake()
    {
        PlayerContainer.limpiarArray();
    }

    #region Métodos para iniciar el juego o el Lab
    /// <summary>
    /// Método llamado desde el selector de jugadores
    /// </summary>
    public void Jugar()
    {
        int escena = Random.Range(5, 5);
        SceneManager.LoadScene("Stage" + escena);
    }

    /// <summary>
    /// Método con el que se inicia una partida personalizada para testear
    /// </summary>
    public void Lab()
    {
        SceneManager.LoadScene("Lab");
    }
    #endregion

    #region Métodos para mantener el focus del mando
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
    public void abrirAyuda()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(helpFirstButton);
    }
    public void cerrarAyuda()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(helpCloseButton);
    }
    public void cerrarSelector()
    {
        //Limpiar el elemento actualmente seleccionado
        EventSystem.current.SetSelectedGameObject(null);

        //Asignar un nuevo elemento del nuevo panel
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }
    #endregion

    /// <summary>
    /// Método para cerrar el juego (desactivado dentro de Unity)
    /// </summary>
    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

}
