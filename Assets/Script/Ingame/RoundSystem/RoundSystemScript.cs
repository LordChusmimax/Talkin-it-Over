using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundSystemScript : MonoBehaviour
{

    private static RoundSystemScript current;
    private static string[] stagesLoaded;
    private static string previousStage = "";
    private static int numRepeat = 0;

    private void Awake()
    {
        current = this;
    }

    public void deletedPlayer(int idPlayer)
    {
        nextRound();
    }


    /// <summary>
    /// M�todo donde se comprobar� los mapas dentro del directorio y se
    /// seleccionar� uno de ellos de forma aleatoria.
    /// </summary>
    public void nextRound()
    {
        //Comprobamos si la lista de mapas est� cargada, en caso de que 
        //no lo est� se rellenar� el array con los elementos del directorio.
        if (stagesLoaded == null) { fillStages(); }

        //Seleccionamos uno de los mapas aleatoriamente mediante la funci�n
        //de 'Random'.
        int numStageSelected = Random.Range(0, stagesLoaded.Length);

        //Seleccionamos el mapa del array obtenido en la funci�n 'Random'.
        string stage = stagesLoaded[numStageSelected];
        
        //Comprobamos si el mapa ha salido previamente y si se ha repetido
        //por segunda vez.
        if (previousStage.Equals(stage)){ numRepeat++; }
        else { numRepeat = 0; }

        previousStage = stage;

        //En caso de que el mapa haya salido por tercera vez, se modificar� 
        //a otro mapa.
        if (numRepeat >= 2)
        {
            //Tomamos un valor aleatorio que incrementar� el valor del mapa seleccionado
            int aux = Random.Range(0, stagesLoaded.Length - 1);
            numStageSelected += aux;

            //En caso de que el valor sea mayor al n�mero de mapas totales, reducimos su valor
            if (numStageSelected > stagesLoaded.Length) { numStageSelected -= stagesLoaded.Length; }

            //Ahora seleccionamos el nuevo mapa en el array y reiniciamos el contador
            stage = stagesLoaded[numStageSelected];
            numRepeat = 0;

            //Cargamos el mapa
            SceneManager.LoadScene(stage);
            Debug.Log("INFO: Mapa reemplazado");
        }

        //Cargamos el mapa
        SceneManager.LoadScene(stage);
    }


    /// <summary>
    /// M�todo en el que se buscar� dentro del directorio de 'Stages' donde 
    /// se obtendr� los archivos de los escenarios y se guardar�n en el array.
    /// </summary>
    private void fillStages()
    {
        //Le guardamos la ruta hasta el propio proyecto del juego con la funci�n
        //'Application.dataPath' y, a partir de este, seleccionamos el directorio
        //donde se guardan los mapas del juego y lo guardamos el la variable 'levelFolder'
        string AssetsFolderPath = Application.dataPath;
        string levelFolder = AssetsFolderPath + "/Scenes/Stages";

        //Creamos una variable de tipo 'DirecotryInfo' donde le pasaremos por par�metro
        //la ruta del directorio creado previamente e inicializamos el array 'files' con
        //la funci�n 'GetFiles()'.
        DirectoryInfo dir = new DirectoryInfo(levelFolder);
        FileInfo[] files = dir.GetFiles("*.unity");

        //Inicializamos el array con la cantidad de archivos encontrados en el directorio.
        int numFiles = files.Length;
        stagesLoaded = new string[numFiles];

        //Creamos un bucle donde guardaremos cada archivo de la lista sin su correspondiente
        //extensi�n en el array.
        for (int i = 0; i < numFiles; i++)
        {
            //Obtenemos el nombre del archivo, le quitamos la extensi�n y lo guardamos
            string fileWithExtension = files[i].Name;
            string fileWithoutExtension = fileWithExtension.Replace(".unity", "");
            stagesLoaded[i] = fileWithoutExtension;
        }
    }

}
