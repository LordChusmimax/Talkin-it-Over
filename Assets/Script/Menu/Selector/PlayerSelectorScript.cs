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
    
    [SerializeField]
    private PanelScript panelScript;

    [SerializeField]
    private MenuScript menuScript;


    private void Awake()
    {
        initInputs();
        deviceConnected = new int[4];
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    /// <summary>
    /// Inicializamos el sistema de Inputs y el de reconocimiento
    ///  en caso de  conexión y desconexión de los mandos.
    /// </summary>
    private void initInputs()
    {
        //Inicializamos el Script de inputs
        inputs = new PlayerInputs();

        //Asignamos los inputs a las funciones especíificas
        inputs.Menu.Asignar.performed += ctxConectar => conectarJugador(ctxConectar.control.device);
        inputs.Menu.Desasignar.performed += ctxDesconectar => desconectarJugador(ctxDesconectar.control.device);
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
    /// Inicializamos al jugador en función del mando que realice el Input.
    /// Si ya existe el mando asignado a un jugador se omite el proceso. 
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void conectarJugador(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int firstPositionFree = 0;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //y comprobamos cual es la posición libre mas baja en el array
        if (numDeviceConnected != 0)
        {
            bool lowest = false;
            int i = 0;

            //Comprobamos si el mando ya está conectado
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

        //Si no está conectado, añádelo al array
        //En caso de que lo esté no hagas nada
        if (isConnected) { return; }

        //Introducimos el id del controlador al array
        deviceConnected[firstPositionFree] = idDevice;
        numDeviceConnected++;

        //Ejecutamos el método del Script para modificar el panel
        panelScript.modifyPanel(true, firstPositionFree, 0);


    }

    
    /// <summary>
    /// Desactivamos el jugador en función del controlador pulsado.
    /// Si no existe dentro del array se omite el proceso
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void desconectarJugador(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int deviceArrayPosition = -1;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //En caso de encontrarlo la variable será 'True', sino será 'False'
        if (numDeviceConnected != 0)
        {
            int i = 0;
            
            //Comprobamos si el mando ya está conectado
            foreach (var valor in deviceConnected)
            {
                //En caso de coincidencia modificamos el bool y guardamos su posición en el array
                if (valor == idDevice) 
                {
                    isConnected = true;
                    deviceArrayPosition = i;
                }

                i++;
            }
        }

        //Si está conectado lo saca del array
        //Si no está conectado no hagas nada
        if (!isConnected) { return; }

        //Cambiamos el valor del array a 0 (sin controlador) y restamos
        //el número de controladores conectados
        deviceConnected[deviceArrayPosition] = 0;
        numDeviceConnected--;

        //Ejecutamos el método del Script para modificar el panel
        panelScript.modifyPanel(false, deviceArrayPosition, 0);
    }

    /// <summary>
    /// Método que llamará al método del script 'PanelScript' para que modifique
    /// la skin en función de la dirección recibida
    /// </summary>
    /// <param name="device">Controlador que ha realizado el Input</param>
    private void cambiarSkin(InputDevice device)
    {
        bool isConnected = false;
        int idDevice = device.deviceId;
        int deviceArrayPosition = -1;

        //Comprobamo si hay al menos un mando conectado, modificando el booleano 'isConnected'
        //En caso de encontrarlo la variable será 'True', sino será 'False'
        if (numDeviceConnected != 0)
        {
            int i = 0;

            //Comprobamos si el mando ya está conectado
            foreach (var valor in deviceConnected)
            {
                //En caso de coincidencia modificamos el bool y guardamos su posición en el array
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
    /// En caso de haber el número de jugadores mínimos se iniciará
    /// la partida guardando las Skins de cada jugador y cargando la escena
    /// </summary>
    /// <param name="obj">Null</param>
    private void empezarPartida(InputAction.CallbackContext obj)
    {
        //Bloqueamos si no ha suficientes jugadores listos
        //if (numDeviceConnected < 2) { return; }

        //Guardamos las skins asignadas
        int[] skins = panelScript.getArray();

        //Con un bucle 'for' recorremos tanto el array de los controladores
        //conectados como el de las skins
        for (int i = 0; i < deviceConnected.Length; i++)
        {
            //Si la posición está vacia pasa a la siguiente instancia
            if (deviceConnected[i].Equals(0)) { continue; }

            //Restamos en 1 para adaptarlo a la llamada en el array de los demas Script
            int adaptedSkin = skins[i] - 1;
            int controllerPosition = 0;

            //Modificamos el controlador del teclado para adaptarlo a la solicitud
            if (deviceConnected[i].Equals(1)) {
                deviceConnected[i] = -1;
                //Debug.Log("Se ha modificado el controlador a: " + deviceConnected[i]);

                //Llamamos al método del Script 'ayadirControler' para guardar la información
                PlayerContainer.ayadirControler(deviceConnected[i], adaptedSkin);
            }
            else
            {
                //Si no es el teclado guardamos la posición del controlador
                controllerPosition = GetGamepadArrayPosition(deviceConnected[i]);
                PlayerContainer.ayadirControler(controllerPosition, adaptedSkin);
            }
        }

        //Llamamos al método de 'Jugar' del Script 'menuScript' para que cargue
        //la escena del juego con los datos introducidos.
        menuScript.Jugar();

    }

    /// <summary>
    /// Método que, al pasarle por parámetro el id del controlador
    /// devolverá la posición asignada en el array de Unity
    /// </summary>
    /// <param name="id">
    ///     int - id del controlador
    /// </param>
    /// <returns>
    ///     int - Posición en el array del controlador
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
    /// Método que mostrará por consola el estado actual del array
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
