using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    private Keyboard teclado;
    private Gamepad[] mandos = new Gamepad[4];
    private int numMandos = 0;
    private int numControllersAsigned = 0;
    private PlayerInputs input;


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
        input = null;
    }

    private void asignarJugador(InputAction.CallbackContext ctx)
    {
        int id = ctx.control.device.deviceId;
        Gamepad mando = GetGamepad(id);

        if (mando == null)
        {
            asignarTeclado();
            numControllersAsigned++;
        }
        else
        {
            //¿Hay algún mando ya iniciado?
            if (numMandos > 0)
            {
                //Comprobar si el mando pulsado conincide con el existente
                bool finded = false;
                for (int i = 0; i < numMandos; i++)
                {
                    if (mando.Equals(mandos[i]))
                    {
                        finded = true;
                    }
                }

                if (!finded)
                {
                    Debug.Log("INFO: Se ha asignado un mando");
                    transform.GetChild(numControllersAsigned).GetComponent<Image>().color = Color.red;

                    PlayerContainer.ayadirControler(GetGamepadArrayPosition(id));

                    mandos[numMandos] = mando;
                    numMandos++;
                    numControllersAsigned++;
                }
                else
                {
                    Debug.Log("INFO: El mando pulsado ya se encuentra activo");
                }

            }
            else
            {
                Debug.Log("INFO: Se ha asignado un mando");
                transform.GetChild(numControllersAsigned).GetComponent<Image>().color = Color.red;

                PlayerContainer.ayadirControler(GetGamepadArrayPosition(id));

                mandos[numMandos] = mando;
                numMandos++;
                numControllersAsigned++;
            }
        }

    }

    private void asignarTeclado()
    {
        if (teclado == null)
        {
            Debug.Log("INFO: Se ha añadido el teclado");

            transform.GetChild(numMandos).GetComponent<Image>().color = Color.red;
            PlayerContainer.ayadirControler(-1);

            teclado = Keyboard.current;
        }
    }
    
    private void desasignarJugador(InputAction.CallbackContext ctx)
    {
        //¿Hay algún controlador ya asignado?
        if (numMandos > 0)
        {
            int id = ctx.control.device.deviceId;
            Gamepad gamepadPressed = GetGamepad(id);

            //¿Es un teclado?
            if (gamepadPressed == null)
            {
                numControllersAsigned--;
                transform.GetChild(numControllersAsigned).GetComponent<Image>().color = Color.green;
                
                PlayerContainer.eliminarControler(-1);
                teclado = null;

                Debug.Log("INFO: Se ha eliminado el controlador del teclado");
            }
            else
            {
                for (int i = 0; i < numMandos; i++)
                {
                    //¿Es el mando que has pulsado?
                    if (gamepadPressed.Equals(mandos[i]))
                    {
                        numMandos--;
                        numControllersAsigned--;
                        transform.GetChild(numControllersAsigned).GetComponent<Image>().color = Color.green;

                        int numIndex = Array.IndexOf(mandos, gamepadPressed);
                        //mandos = mandos.Where((val, idx) => idx != numIndex).ToArray();
                        mandos = mandos.Except(new Gamepad[] { gamepadPressed }).ToArray();

                        int aux = GetGamepadArrayPosition(id);
                        PlayerContainer.eliminarControler(aux);

                        Debug.Log("INFO: Se ha eliminado el controlador del mando");

                        int[] lista = PlayerContainer.getArray();

                        for (int j = 0; j < lista.Length; j++)
                        {
                            Debug.Log("Elemento: " + j + " - Valor: " + lista[j]);
                        }

                        Debug.Log(PlayerContainer.getNumController());

                    }
                }
            }

        }
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
}
