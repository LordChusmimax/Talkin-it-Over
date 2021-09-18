using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSelectorScript : MonoBehaviour
{
    private PlayerInputs inputs;
    private int[] deviceConnected;
    private int numDeviceConnected = 0;
    private int timeWait = 2;
    private Coroutine coroutine;
    private bool ready;
    
    [SerializeField] private PanelScript panelScript;
    [SerializeField] private MenuScript menuScript;
    [SerializeField] private GameObject txtEmpezar;
    [SerializeField] private GameObject menu;


    private void Awake()
    {
        initInputs();
        deviceConnected = new int[4];
    }

    private void OnEnable()
    {
        inputs.Enable();
        actualizarAviso();
    }

    private void OnDisable()
    {
        //Desactivamos los inputs del selector
        inputs.Disable();

        //Comprobamos si existe la corrutina de salida del selector y al destruimos
        if (coroutine != null) { StopCoroutine(coroutine); }
    }

    /// <summary>
    /// Inicializamos el sistema de Inputs y el de reconocimiento
    ///  en caso de  conexi�n y desconexi�n de los mandos.
    /// </summary>
    private void initInputs()
    {
        //Inicializamos el Script de inputs
        inputs = new PlayerInputs();

        //Asignamos los inputs a las funciones espec�ificas
        inputs.Menu.Asignar.performed += ctxConectar => conectarJugador(ctxConectar.control.device);
        inputs.Menu.Desasignar.performed += ctxDesconectar => desconectarJugador(ctxDesconectar.control.device);
        inputs.Menu.Desasignar.canceled += ctxSoltarBoton => soltarBoton();
        inputs.Menu.CambiarSkin.performed += ctxCambiarSkin => cambiarSkin(ctxCambiarSkin.control.device);
        inputs.Menu.Empezar.performed += empezarPartida;

        //Iniciamos el sistema de Inputs
        inputs.Enable();


        //Implementados un sistema para controlar cuando los controles se desactivan
        InputSystem.onDeviceChange += (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    conectarJugador(device);
                    break;

                case InputDeviceChange.Removed:
                    desconectarJugador(device);
                    break;
            }
        };

    }

    /// <summary>
    /// Inicializamos al jugador en funci�n del mando que realice el Input.
    /// Si ya existe el mando asignado a un jugador se omite el proceso. 
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void conectarJugador(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int firstPositionFree = 0;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //y comprobamos cual es la posici�n libre mas baja en el array
        if (numDeviceConnected != 0)
        {
            bool lowest = false;
            int i = 0;

            //Comprobamos si el mando ya est� conectado
            foreach (var valor in deviceConnected)
            {
                if (valor == idDevice)
                {
                    isConnected = true;
                }

                if (!lowest && valor == 0)
                {
                    lowest = true;
                    firstPositionFree = i;
                }

                i++;
            }
        }

        //Si no est� conectado, a��delo al array
        //En caso de que lo est� no hagas nada
        if (isConnected) { return; }

        //Introducimos el id del controlador al array
        deviceConnected[firstPositionFree] = idDevice;
        numDeviceConnected++;

        //Ejecutamos el m�todo del Script para modificar el panel
        panelScript.modifyPanel(true, firstPositionFree, 0);

        actualizarAviso();
    }

    
    /// <summary>
    /// Desactivamos el jugador en funci�n del controlador pulsado.
    /// Si no existe dentro del array se omite el proceso
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void desconectarJugador(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int deviceArrayPosition = -1;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //En caso de encontrarlo la variable ser� 'True', sino ser� 'False'
        if (numDeviceConnected != 0)
        {
            int i = 0;
            
            //Comprobamos si el mando ya est� conectado
            foreach (var valor in deviceConnected)
            {
                //En caso de coincidencia modificamos el bool y guardamos su posici�n en el array
                if (valor == idDevice) 
                {
                    isConnected = true;
                    deviceArrayPosition = i;
                }

                i++;
            }
        }

        //Si est� conectado lo saca del array
        //Si no est� conectado lanza la corrutina de vuelta al men�
        if (!isConnected) 
        {
            //Iniciamos la corrutina y la guardamos en la varable 'corutine'
            coroutine = StartCoroutine(cerrarSelector(timeWait));
            return;
        }

        //Cambiamos el valor del array a 0 (sin controlador) y restamos
        //el n�mero de controladores conectados
        deviceConnected[deviceArrayPosition] = 0;
        numDeviceConnected--;

        //Ejecutamos el m�todo del Script para modificar el panel
        panelScript.modifyPanel(false, deviceArrayPosition, 0);

        actualizarAviso();
    }

    /// <summary>
    /// M�todo que llamar� al m�todo del script 'PanelScript' para que modifique
    /// la skin en funci�n de la direcci�n recibida
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void cambiarSkin(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int deviceArrayPosition = -1;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //En caso de encontrarlo la variable ser� 'True', sino ser� 'False'
        if (numDeviceConnected != 0)
        {
            int i = 0;

            //Comprobamos si el mando ya est� conectado
            foreach (var valor in deviceConnected)
            {
                //En caso de coincidencia modificamos el bool y guardamos su posici�n en el array
                if (valor == idDevice)
                {
                    isConnected = true;
                    deviceArrayPosition = i;
                }

                i++;
            }
        }

        //Comprobamos si el mando se encuentra activo en el array
        if (!isConnected) { return; }

        int operation = (int) inputs.Menu.CambiarSkin.ReadValue<float>();
        panelScript.assignSkin(deviceArrayPosition, operation);

    }

    /// <summary>
    /// En caso de haber el n�mero de jugadores m�nimos se iniciar�
    /// la partida guardando las Skins de cada jugador y cargando la escena
    /// </summary>
    /// <param name="obj">Null</param>
    private void empezarPartida(InputAction.CallbackContext obj)
    {
        //Bloqueamos si no ha suficientes jugadores listos
        if (!ready) { return; }

        //Guardamos las skins asignadas
        int[] skins = panelScript.getArray();

        //Con un bucle 'for' recorremos tanto el array de los controladores
        //conectados como el de las skins
        for (int i = 0; i < deviceConnected.Length; i++)
        {
            //Si la posici�n est� vacia pasa a la siguiente instancia
            if (deviceConnected[i].Equals(0)) { continue; }

            //Restamos en 1 para adaptarlo a la llamada en el array de los demas Script
            int adaptedSkin = skins[i] - 1;
            int controllerPosition = 0;

            //Modificamos el controlador del teclado para adaptarlo a la solicitud
            if (deviceConnected[i].Equals(1)) {
                deviceConnected[i] = -1;
                //Debug.Log("Se ha modificado el controlador a: " + deviceConnected[i]);

                //Llamamos al m�todo del Script 'ayadirControler' para guardar la informaci�n
                PlayerContainer.ayadirControler(deviceConnected[i], adaptedSkin);
            }
            else
            {
                //Si no es el teclado guardamos la posici�n del controlador
                controllerPosition = GetGamepadArrayPosition(deviceConnected[i]);
                PlayerContainer.ayadirControler(controllerPosition, adaptedSkin);
            }
        }

        //Llamamos al m�todo de 'Jugar' del Script 'menuScript' para que cargue
        //la escena del juego con los datos introducidos.
        menuScript.Jugar();

    }

    /// <summary>
    /// M�todo que, al pasarle por par�metro el id del controlador
    /// devolver� la posici�n asignada en el array de Unity
    /// </summary>
    /// <param name="id">
    ///     int - id del controlador
    /// </param>
    /// <returns>
    ///     int - Posici�n en el array del controlador
    /// </returns>
    private int GetGamepadArrayPosition(int id)
    {
        var mandos = Gamepad.all;
        int getMandoPosition = -1;

        for (int i = 0; i < mandos.Count; i++)
        {
            int idMando = mandos[i].deviceId;

            if (id.Equals(idMando))
            {
                getMandoPosition = i;
            }
        }

        return getMandoPosition;
    }

    /// <summary>
    /// M�todo donde se comprobar� el n�mero de jugadores conectados
    /// y se actualizar� la variable de 'ready', ademas de cambiar el texto de aviso
    /// </summary>
    private void actualizarAviso()
    {
        //Comprobamos el n�mero de jugadores conectados
        if (numDeviceConnected >= 2)
        {
            //Actualizamos el aviso del texto y modificamos la variable de 'ready'
            actualizarTexto("empezar");
            ready = true;

            return;
        }

        actualizarTexto("unirse");
        ready = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clave">
    ///     string - Texto que se usar� para buscar el elemento del diccionario.
    /// </param>
    private void actualizarTexto(string clave)
    {
        //Modificamos el id del componenete y actualizamos el aviso
        txtEmpezar.GetComponent<SeleccionarTexto>().id = clave;
        txtEmpezar.GetComponent<SeleccionarTexto>().modificarTexto();
    }

    /// <summary>
    /// M�todo lanzado al pulsar el bot�n de salir sin estar conectado el mando donde,
    /// tras unos segundos se desactivar� el panel de selecci�n y se activar� el del men�
    /// </summary>
    /// <param name="tiempo">
    ///     int - Tiempo en segundos que requerir� para activar el evento
    /// </param>
    /// <returns></returns>
    IEnumerator cerrarSelector(int tiempo)
    {
        //Mantenemos la corrutina activa con un bucle infinito
        while (true)
        {
            //Comprobamos si el tiempo ha llegado a 0
            if (tiempo < 0)
            {
                //Activamos el panel del men� y cerramos el selector de personajes
                menu.SetActive(true);
                menuScript.cerrarSelector();

                //Desactivamos el panel del selector
                this.gameObject.SetActive(false);
            }

            //Modificamos el texto avisando del tiempo restante
            txtEmpezar.GetComponent<SeleccionarTexto>().id = "volviendo";
            txtEmpezar.GetComponent<SeleccionarTexto>().modificarTexto((tiempo + 1).ToString());

            //Hacemos que la corrutina eespere un segundo
            yield return new WaitForSeconds(1);

            //Reducimos el tiempo restante
            tiempo--;
        }

    }

    /// <summary>
    /// M�todo lanzado al soltar el bot�n de retroceso del controlador.
    /// Si existe una corrutina activa para volver al men� la destruye y actualiza el
    /// mensaje del texto.
    /// </summary>
    private void soltarBoton()
    {
        //Comprobamos si existe una corrutina activa
        if (coroutine != null)
        {
            Debug.Log(">>>INFO: Se ha detenido la corrutina: " + coroutine);
            
            //Destruimos la corrutina, vaciamos la variable y actualizamos el texto.
            StopCoroutine(coroutine);
            coroutine = null;
            actualizarAviso();
        }

    }

    /// <summary>
    /// M�todo que mostrar� por consola el estado actual del array
    /// </summary>
    private void showArray()
    {
        string list = "Controladores conectados: {";
        
        foreach (var valor in deviceConnected)
        {
            list += valor + " ";
        }

        list += "}";

        Debug.Log(list);
    }

}
