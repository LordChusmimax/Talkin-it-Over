using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersSelector : MonoBehaviour
{
    private Dictionary<int, int> controles = new Dictionary<int, int>();
    private Queue<int> skins = new Queue<int>();
    private Stack<int> paneles = new Stack<int>();
    private PlayerInputs input;
    [SerializeField] private GameObject menu;
    [SerializeField] private MenuScript menuScript;

    private void Awake()
    {
        input = new PlayerInputs();

        input.Player.Asignar.performed += ctxAsignar => asignarJugador(ctxAsignar);
        input.Player.Desasignar.performed += ctxDesasignar => desasignarJugador(ctxDesasignar);
        input.Player.CambiarSkin.performed += ctxCambiar => cambiarSkin(ctxCambiar);
        input.Player.Empezar.performed += ctxEmpezar => empezarJuego();

        paneles.Push(3);
        paneles.Push(2);
        paneles.Push(1);
        paneles.Push(0);

        skins.Enqueue(0);
        skins.Enqueue(1);
        skins.Enqueue(2);
        skins.Enqueue(3);

        input.Enable();
    }

    private void OnEnable()
    {
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
                int numPanel = paneles.Peek();
                controles.Add(-1, numPanel);

                Transform miPanel = transform.GetChild(numPanel);
                miPanel.GetComponent<Image>().color = Color.red;
                miPanel.GetChild(1).GetComponent<Image>().color = getColorInInt(skins.Peek());

                skins.Dequeue();
                paneles.Pop();
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
                int numPanel = paneles.Peek();
                controles.Add(GetGamepadArrayPosition(id), numPanel);

                Transform miPanel = transform.GetChild(numPanel);
                miPanel.GetComponent<Image>().color = Color.red;
                miPanel.GetChild(1).GetComponent<Image>().color = getColorInInt(skins.Peek());

                skins.Dequeue();
                paneles.Pop();

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
        
        if (controles.ContainsKey(key))
        {
            foreach (KeyValuePair<int, int> control in controles)
            {
                if (control.Key.Equals(key))
                {
                    Transform miPanel = transform.GetChild(control.Value);
                    miPanel.GetComponent<Image>().color = Color.green;
                    skins.Enqueue(getIntInColor(miPanel.GetChild(1).GetComponent<Image>().color));

                    miPanel.GetChild(1).GetComponent<Image>().color = Color.green;

                    paneles.Push(control.Value);
                    controles.Remove(key);
                    break;
                }
            }
        }
        else
        {
            //Debug.Log("ERROR: Este controlador no se encuentra actualmente asignado");

            //menu.SetActive(true);
            //menuScript.cerrarSelector();

            //this.gameObject.SetActive(false);
        }
    }

    private void cambiarSkin(InputAction.CallbackContext ctx)
    {
        int key = GetGamepadArrayPosition(ctx.control.device.deviceId);

        if (controles.ContainsKey(key))
        {
            foreach (KeyValuePair<int, int> control in controles)
            {

                if (control.Key.Equals(key))
                {

                    Transform miPanel = transform.GetChild(control.Value);

                    int aux = getIntInColor(miPanel.GetChild(1).GetComponent<Image>().color);
                    miPanel.GetChild(1).GetComponent<Image>().color = getColorInInt(skins.Dequeue());
                    skins.Enqueue(aux);
                }

            }
        }
        else
        {
            Debug.Log("ERROR: Este controlador no se encuentra actualmente asignado");
        }
    }

    private Color getColorInInt(int numSkin)
    {
        switch (numSkin)
        {
            case 0:
                return Color.magenta;

            case 1:
                return Color.yellow;

            case 2:
                return Color.blue;

            case 3:
                return Color.cyan;

            default:
                return Color.black;
        }
    }

    private int getIntInColor(Color miColor)
    {
        int aux = -1;

        if (miColor.Equals(Color.magenta))
        {
            aux = 0;
        }

        if (miColor.Equals(Color.yellow))
        {
            aux = 1;
        }

        if (miColor.Equals(Color.blue))
        {
            aux = 2;
        }

        if (miColor.Equals(Color.cyan))
        {
            aux = 3;
        }

        return aux;
    }

    private void empezarJuego()
    {
        menuScript.Jugar();
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
