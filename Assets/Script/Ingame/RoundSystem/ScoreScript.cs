using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private static ScoreScript current;

    private void Awake()
    {
        //Comprobamos si ya hay otro objeto instanciado
        deleteDuplicate();
    }

    /// <summary>
    /// M�todo que comprobar� si existe el mismo objeto en escena.
    /// Si existe lo elimina, si no se guardar� la variable est�tica.
    /// </summary>
    private void deleteDuplicate()
    {
        //Si existe el objeto ya en escena elimina el nuevo
        if (current != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        //Inicializamos la variable de 'current' y evitamos que el objeto sea
        //destruido al iniciar la nueva escena
        current = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Asignado nuevo objeto");
    }
}
