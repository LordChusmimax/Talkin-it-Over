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


    private void Awake()
    {
        initInputs();
        deviceConnected = new int[4];
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
        inputs.Menu.Desasignar.performed += ctxConectar => desconectarJugador(ctxConectar.control.device);
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
        if (!isConnected)
        {
            //Introducimos el id del controlador al array
            deviceConnected[firstPositionFree] = idDevice;
            numDeviceConnected++;

            //Ejecutamos el método del Script para modificar el panel
            panelScript.addPlayereInPanel(firstPositionFree);

            //Mostramos el estado actual del array
            //showArray();

            //Finalizamos la ejecución del método
            return;
        }

        //Mandamos un mensaje de aviso al desarrollador
        //Debug.Log("INFO: El controlador " + idDevice + " ya está conectado");

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
        if (isConnected)
        {
            //Cambiamos el valor del array a 0 (sin controlador) y restamos
            //el número de controladores conectados
            deviceConnected[deviceArrayPosition] = 0;
            numDeviceConnected--;

            //Ejecutamos el método del Script para modificar el panel
            panelScript.removePlayerinPanel(deviceArrayPosition);

            //Mostramos el estado actual del array
            //showArray();

            return;
        }

        //Mandamos un mensaje de aviso al desarrollador
        //Debug.Log("INFO: El controlador " + idDevice + " ya está desconectado");

    }

    /// <summary>
    /// En caso de haber el número de jugadores mínimos se iniciará
    /// la partida guardando las Skins de cada jugador y cargando la escena
    /// </summary>
    /// <param name="obj">Null</param>
    private void empezarPartida(InputAction.CallbackContext obj)
    {


    }

    /// <summary>
    /// Método que mostrará por consola el estado actual del array
    /// </summary>
    private void showArray()
    {
        string list = " {";
        
        foreach (var valor in deviceConnected)
        {
            list += valor + " ";
        }

        list += "}";

        Debug.Log(list);
    }

}
