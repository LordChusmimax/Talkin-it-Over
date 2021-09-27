using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteSkins;
    private int[] skinsAsigned;
    private int numSkinsActive = 0;
    private int numSkins;
    private Transform myPanel;

    private void Awake()
    {
        //Guardamos el n�mero de skins qu habr� en el juego
        numSkins = spriteSkins.Length;
        
        //Creamos una lista donde indicaremos las skins que estar�n usadas
        //por los jugadores (4 espacios para 4 jugadores)
        skinsAsigned = new int[4];

    }

    /// <summary>
    /// Funci�n al que se le indicar� si tiene que activar o desactivar un panel
    /// y cual deber� modificar
    /// </summary>
    /// <param name="active">
    /// True -> Activar panel
    /// False -> Desactivar panel
    /// </param>
    /// <param name="panelPosition">Panel a modificar</param>
    public void modifyPanel(bool active, int panelPosition, int operation)
    {
        //Guardamos el objeto panel que vamos a modificar
        myPanel = transform.GetChild(panelPosition);

        //Modificamos el estado del objeto panel
        GameObject aux = myPanel.gameObject;
        aux.SetActive(active);

        //Compribamos si se ha activado el panel
        if (active)
        {
            //Asignamos una skin por defecto al jugador reci�n conectado y finalizamos el m�todo
            assignSkin(panelPosition, operation);

            //Mostramos en consola el estado de la lista
            //showArray();
            return;
        }

        //En caso contrario desactivamos la skin
        clearSkins(panelPosition);

        //Mostramos en consola el estado de la lista
        //showArray();
    }

    
    /// <summary>
    /// Funci�n que recibe como par�metro la posici�n del jugador
    /// en el array que realiaz el Input y le asigna o modifica la skin.
    /// </summary>
    /// <param name="panelPosition">
    /// Indica que posici�n del array va a realizar la modificaci�n
    /// </param>
    /// <param name="operation">
    /// Indica que tipo de modificaci�n se va a realizar
    ///     1 - Incremetar valor Skin
    ///     -1 - Decrementar valor Skin
    ///     0 - Asignar por defecto
    /// </param>
    public void assignSkin(int panelPosition, int operation)
    {
        //Comprobamos si el n�mero de skins activas es igual o mayor
        //al total, si es as� bloqueamos la acci�n de a�adir o modificar la skin
        if (numSkinsActive >= numSkins) { return; }

        //Comprobamos si la operaci�n es asignar una skin por defecto
        if (operation.Equals(0))
        {
            //Obtenemos la primera skin disponible y se la asignamos al array
            int skinSelected = getFirstSkinFree();
            skinsAsigned[panelPosition] = skinSelected;

            //Restamos en 1 el valor para hacer referencia correctamente al array de skins
            skinSelected--;
            myPanel.GetChild(1).GetComponent<Image>().sprite = spriteSkins[skinSelected];

            //Sumamos 1 al valor de skins activas
            numSkinsActive++;

            //showArray();

            //Finalizamos la ejecuci�n del m�todo
            return;
        }

        //Seleccionamos el panel que vamos a modificar
        myPanel = transform.GetChild(panelPosition);

        //Compribamos la siguiente skin disponible, guardamos su valor y
        //asignamos el Sprite al panel
        int skinChanged = changeSkin(operation, panelPosition);
        myPanel.GetChild(1).GetComponent<Image>().sprite = spriteSkins[skinChanged -1];

        //showArray();
    }

    /// <summary>
    /// M�todo que nos devuelve el valor en entero de la siguiente skin
    /// disponible en funci�n de la operaci�n solicitada
    /// </summary>
    /// <param name="operation">
    ///     1 o -1 en funci�n de la direcci�n del Input
    /// </param>
    /// <param name="panelPosition">
    ///     Panel que va a realizar la modificaci�n
    /// </param>
    /// <returns></returns>
    private int changeSkin(int operation, int panelPosition)
    {
        //Obtener la skin acualmete usada
        int actualSkin = skinsAsigned[panelPosition];
        bool avalibleSkin = false;
        int vueltas = 0;
        int topeVueltas = numSkins * 2;

        //Debug.Log("INFO: Skin actual: " + actualSkin);

        //Buscamos en el array por la siguiente skin disponible
        do
        {
            //Ponemos un tope de seguridad para evitar bucles infinitos
            if (vueltas >= topeVueltas) {
                Debug.Log("ERROR: Salida de emergencia en bucle de skins");
                //avalibleSkin = true;
                return 0;
            }
            
            //Modificamos la skin que queremos en funci�n de la operaci�n a realizar
            actualSkin += operation;

            //Comprobamo que el nuevo valor no sobrepasa los l�mites
            if (actualSkin <= 0) { actualSkin = numSkins; }
            if (actualSkin > numSkins) { actualSkin = 1; }

            //Comprobamos si la skin solicitada est� en uso
            foreach (var skinInArray in skinsAsigned)
            {
                //Continuar al siguiente elemento si no hay juagdor con skin
                if (skinInArray.Equals(0)) { continue; }
                //Cerrar el bucle si se ha encontrado en uso la skin solicitada
                if (skinInArray.Equals(actualSkin))
                {
                    //Debug.Log("INFO: Coincidencia encontrada en la skin: " + actualSkin);
                    avalibleSkin = false;
                    break;
                }

                avalibleSkin = true;
            }

            //Incrementamos en n�mero devueltas dadas
            vueltas++;

        } while (!avalibleSkin);

        //Modificamos el valor de la skin en el array
        skinsAsigned[panelPosition] = actualSkin;
        //showArray();

        return actualSkin;
    }

    /// <summary>
    /// M�todo que moifica el array asignando el valor a 0 de nuevo
    /// y reduciendo el n�mero de skins activas
    /// </summary>
    /// <param name="panelPosition">
    /// Indica que posici�n del array va a realizar la modificaci�n
    /// </param>
    private void clearSkins(int panelPosition)
    {
        //Asignamos al la correspondiente posici�n del array el valor de 0
        skinsAsigned[panelPosition] = 0;

        //Reducimos en 1 el valor de skins activas
        numSkinsActive--;
    }

    /// <summary>
    /// M�todo donde se recorre el array de las skins asignadas en
    /// busca de la siguiente menor disponible
    /// </summary>
    /// <returns>
    ///     int - Valor de la skin disponible de la lista de skins
    /// </returns>
    private int getFirstSkinFree()
    {
        //Si no hay ninguna skin activa se le dar� la primera por defecto
        if (numSkinsActive.Equals(0)) { return 1; }

        //Creamos una variable donde guardamos el valor m�nimo encontrado
        int potentialSkin = 0;

        //Creamos otra variable booleana que comprobar� si se ha localizado
        //una skin viable
        bool avalible = false;

        //Creamos las variables para controlar el n�mero de vueltas e impedir
        //que se genere un bucle infinito.
        int vueltas = 0;
        int topeVueltas = numSkins * 2;

        //Creamos un bucle que nos permitir� comprobar cada skin
        //y ver si est� disponible.
        do
        {

            if (vueltas >= topeVueltas)
            {
                Debug.Log("ERROR: Salida de emergencia en bucle de skins");
                return 1;
            }

            //Buscamos la siguiente skin
            potentialSkin++;

            //Recorremos el array comprobando las skins solicitada
            foreach (var skinInArray in skinsAsigned)
            {
                //Si no hay skin asignada pasa a la siguiente valor
                if (skinInArray.Equals(0)) { continue; }

                //Si la skin ya est� en uso busca por la siguiente skin
                if (skinInArray.Equals(potentialSkin)) 
                {
                    avalible = false;
                    break;
                }

                avalible = true;
            }

            //Guardamos el n�mero de vueltas dadas en el bucle.
            vueltas++;

        } while (!avalible);

        
        return potentialSkin;
    }

    /// <summary>
    /// M�todo que devuelve el array con las skins asignadas
    /// para cada jugador
    /// </summary>
    /// <returns>
    ///     int[] - Array con los valores de las skins asignadas
    /// </returns>
    public int[] getArray()
    {
        return skinsAsigned;
    }

    /// <summary>
    /// M�todo que nos muestra por consola es estado del array
    /// </summary>
    private void showArray()
    {
        string list = "Skins asignadas: { ";

        foreach (var valor in skinsAsigned)
        {
            list += valor + " ";
        }

        list += "}";

        Debug.Log(list);
    }

}
