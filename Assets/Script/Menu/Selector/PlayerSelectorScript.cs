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
    ///  en caso de  conexi�n y desconexi�n de los mandos.
    /// </summary>
    private void initInputs()
    {
        //Inicializamos el Script de inputs
        inputs = new PlayerInputs();

        //Asignamos los inputs a las funciones espec�ificas
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
        if (!isConnected)
        {
            //Introducimos el id del controlador al array
            deviceConnected[firstPositionFree] = idDevice;
            numDeviceConnected++;

            //Ejecutamos el m�todo del Script para modificar el panel
            panelScript.addPlayereInPanel(firstPositionFree);

            //Mostramos el estado actual del array
            //showArray();

            //Finalizamos la ejecuci�n del m�todo
            return;
        }

        //Mandamos un mensaje de aviso al desarrollador
        //Debug.Log("INFO: El controlador " + idDevice + " ya est� conectado");

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
        //Si no est� conectado no hagas nada
        if (isConnected)
        {
            //Cambiamos el valor del array a 0 (sin controlador) y restamos
            //el n�mero de controladores conectados
            deviceConnected[deviceArrayPosition] = 0;
            numDeviceConnected--;

            //Ejecutamos el m�todo del Script para modificar el panel
            panelScript.removePlayerinPanel(deviceArrayPosition);

            //Mostramos el estado actual del array
            //showArray();

            return;
        }

        //Mandamos un mensaje de aviso al desarrollador
        //Debug.Log("INFO: El controlador " + idDevice + " ya est� desconectado");

    }

    /// <summary>
    /// En caso de haber el n�mero de jugadores m�nimos se iniciar�
    /// la partida guardando las Skins de cada jugador y cargando la escena
    /// </summary>
    /// <param name="obj">Null</param>
    private void empezarPartida(InputAction.CallbackContext obj)
    {


    }

    /// <summary>
    /// M�todo que mostrar� por consola el estado actual del array
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
