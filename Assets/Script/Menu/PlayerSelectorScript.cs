using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectorScript : MonoBehaviour
{
    private PlayerInputs inputs;

    private void Awake()
    {
        initInputs();

    }

    /// <summary>
    /// Inicializamos el sistema de Inputs y el de reconocimiento
    ///  en caso de  conexión y desconexión de los mandos.
    /// </summary>
    private void initInputs()
    {
        //Inicializamos el Script de inputs
        inputs = new PlayerInputs();

        //Asignamos los inputs a las funciones  especíificas
        inputs.Menu.Asignar.performed += ctxConectar => conectarJugador(ctxConectar.control.device);

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
        Debug.Log("Id controlador conectado: " + device.deviceId);
    }

}
