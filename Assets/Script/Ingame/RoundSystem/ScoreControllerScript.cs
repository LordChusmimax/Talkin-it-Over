using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControllerScript : MonoBehaviour
{
    private static ScoreControllerScript current;

    private void Awake()
    {
        //Comprobamos si ya hay otro objeto instanciado
        deleteDuplicate();
    }

    /// <summary>
    /// Método que comprobará si existe el mismo objeto en escena.
    /// En caso de que exista eliminará el nuevo, si no se guardará la variable estática y
    /// se marcará para que no se destruya al cambiar de escena.
    /// </summary>
    private void deleteDuplicate()
    {
        //Si existe el objeto ya en escena elimina el nuevo
        if (current != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        //Inicializamos la variable de 'current', evitamos que el objeto sea
        //destruido al iniciar la nueva escena e inicializamos las variables
        //del sistema de rondas.
        current = this;
        ScoreData.initData();
        DontDestroyOnLoad(this.gameObject);
    }
}
