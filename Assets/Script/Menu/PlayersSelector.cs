using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersSelector : MonoBehaviour
{
    private Dictionary<int, int> controles = new Dictionary<int, int>();
    private PlayerInputs input;
    private int panelPosition = 0;

    private void Awake()
    {
        input = new PlayerInputs();

        input.Player.Asignar.performed += ctxAsignar => asignarJugador(ctxAsignar);
        input.Player.Desasignar.performed += ctxDesasignar => desasignarJugador(ctxDesasignar);

        input.Enable();
    }
    
    private void OnDisable()
    {

        input.Disable();
        foreach (KeyValuePair<int, int> control in controles)
        {
            PlayerContainer.ayadirControler(control.Key, 0);
        }

    }

    private void asignarJugador(InputAction.CallbackContext ctx)
    {
        int id = ctx.control.device.deviceId;

        //¿Es un teclado?
        if (id == 1)
        {
            //¿Está ya asignado?
            try
            {
                controles.Add(-1, panelPosition);
                transform.GetChild(panelPosition).GetComponent<Image>().color = Color.red;
                panelPosition++;
                Debug.Log("INFO: Se ha asignado el teclado correctamente");
            }
            catch (ArgumentException)
            {
                Debug.Log("ERROR: Ya está asignado el teclado");
            }
        }
        else
        {//Es un mando
            try
            {
                controles.Add(GetGamepadArrayPosition(id), panelPosition);
                transform.GetChild(panelPosition).GetComponent<Image>().color = Color.red;
                panelPosition++;
                Debug.Log("INFO: Se ha asignado el mando correctamente");
            }
            catch (ArgumentException)
            {
                Debug.Log("ERROR: Ya está asignado el mando");
            }
        }
    }

    private void desasignarJugador(InputAction.CallbackContext ctx)
    {
        int key = GetGamepadArrayPosition(ctx.control.device.deviceId);
        bool finded = controles.Remove(key);
        
        if (finded)
        {
            panelPosition--;
            transform.GetChild(panelPosition).GetComponent<Image>().color = Color.green;
            Debug.Log("INFO: Se ha desasignado el control correctamente");
        }
        else
        {
            Debug.Log("ERROR: Este controlador no se encuentra actualmente añadido");
        }
    }

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

    private Gamepad GetGamepad(int id)
    {
        var mandos = Gamepad.all;
        Gamepad getMando = null;

        for (int i = 0; i < mandos.Count; i++)
        {
            int idMando = mandos[i].deviceId;

            if (id.Equals(idMando))
            {
                getMando = mandos[i];
            }
        }

        return getMando;
    }

}
