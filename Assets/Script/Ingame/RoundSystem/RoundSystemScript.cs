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
    /// Método donde se comprobará los mapas dentro del directorio y se
    /// seleccionará uno de ellos de forma aleatoria.
    /// </summary>
    public void nextRound()
    {
        //Comprobamos si la lista de mapas está cargada, en caso de que 
        //no lo esté se rellenará el array con los elementos del directorio.
        if (stagesLoaded == null) { fillStages(); }

        //Seleccionamos uno de los mapas aleatoriamente mediante la función
        //de 'Random'.
        int numStageSelected = Random.Range(0, stagesLoaded.Length);

        //Seleccionamos el mapa del array obtenido en la función 'Random'.
        string stage = stagesLoaded[numStageSelected];
        
        //Comprobamos si el mapa ha salido previamente y si se ha repetido
        //por segunda vez.
        if (previousStage.Equals(stage)){ numRepeat++; }
        else { numRepeat = 0; }

        previousStage = stage;

        //En caso de que el mapa haya salido por tercera vez, se modificará 
        //a otro mapa.
        if (numRepeat >= 2)
        {
            //Tomamos un valor aleatorio que incrementará el valor del mapa seleccionado
            int aux = Random.Range(0, stagesLoaded.Length - 1);
            numStageSelected += aux;

            //En caso de que el valor sea mayor al número de mapas totales, reducimos su valor
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
    /// Método en el que se buscará dentro del directorio de 'Stages' donde 
    /// se obtendrá los archivos de los escenarios y se guardarán en el array.
    /// </summary>
    private void fillStages()
    {
        //Le guardamos la ruta hasta el propio proyecto del juego con la función
        //'Application.dataPath' y, a partir de este, seleccionamos el directorio
        //donde se guardan los mapas del juego y lo guardamos el la variable 'levelFolder'
        string AssetsFolderPath = Application.dataPath;
        string levelFolder = AssetsFolderPath + "/Scenes/Stages";

        //Creamos una variable de tipo 'DirecotryInfo' donde le pasaremos por parámetro
        //la ruta del directorio creado previamente e inicializamos el array 'files' con
        //la función 'GetFiles()'.
        DirectoryInfo dir = new DirectoryInfo(levelFolder);
        FileInfo[] files = dir.GetFiles("*.unity");

        //Inicializamos el array con la cantidad de archivos encontrados en el directorio.
        int numFiles = files.Length;
        stagesLoaded = new string[numFiles];

        //Creamos un bucle donde guardaremos cada archivo de la lista sin su correspondiente
        //extensión en el array.
        for (int i = 0; i < numFiles; i++)
        {
            //Obtenemos el nombre del archivo, le quitamos la extensión y lo guardamos
            string fileWithExtension = files[i].Name;
            string fileWithoutExtension = fileWithExtension.Replace(".unity", "");
            stagesLoaded[i] = fileWithoutExtension;
        }
    }

}
