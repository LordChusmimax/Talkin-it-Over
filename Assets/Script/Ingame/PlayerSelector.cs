using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelector : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject playerRespawer;

    private Keyboard teclado;
    private Gamepad[] mandos = new Gamepad[4];
    private int numMandos = 0;
    private PlayerInputs input;


    private void Awake()
    {
        input = new PlayerInputs();

        input.Player.Jump.performed += ctxAsignar => asignarJugador(ctxAsignar);
        input.Player.Pick.performed += ctxGenerar => generarJugadores();

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void asignarJugador(InputAction.CallbackContext ctx)
    {
        Gamepad mando = GetGamepad(ctx.control.device.deviceId);

        if (mando == null)
        {
            asignarTeclado();
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
                    ScriptModifyPanel panel = GetComponentInChildren<ScriptModifyPanel>();
                    panel.ayadirJugador();

                    mandos[numMandos] = mando;
                    numMandos++;
                }
                else
                {
                    Debug.Log("INFO: El mando pulsado ya se encuentra activo");
                }

            }
            else
            {
                Debug.Log("INFO: Se ha asignado un mando");
                ScriptModifyPanel panel = GetComponentInChildren<ScriptModifyPanel>();
                panel.ayadirJugador();

                mandos[numMandos] = mando;
                numMandos++;
            }
        }

    }

    private void asignarTeclado()
    {
        if (teclado == null)
        {
            Debug.Log("INFO: Se ha añadido el teclado");

            ScriptModifyPanel panel = GetComponentInChildren<ScriptModifyPanel>();
            panel.ayadirJugador();

            teclado = Keyboard.current;
        }
    }

    public void generarJugadores()
    {
        //¿Hay algún controls asignado?
        if (numMandos > 0 || teclado != null)
        {
            //ScriptModifyPanel panel = GetComponentInChildren<ScriptModifyPanel>();
            //panel.gameObject.SetActive(false);

            this.gameObject.SetActive(false);
            int[] coor = {0, -2, 2 ,-4};
            int numCoor = 0;
            
            if (teclado != null)
            {
                GameObject player = Instantiate(playerPrefab, playerRespawer.transform);
                player.transform.SetParent(playerRespawer.transform);

                PlayerScript scriptJugador = playerPrefab.GetComponent<PlayerScript>();
                scriptJugador.SelectController(-1);

                playerRespawer.transform.Translate(new Vector2(coor[numCoor], 0));
                numCoor++;
            }

            for (int i = 0; i < numMandos; i++)
            {
                GameObject player = Instantiate(playerPrefab, playerRespawer.transform);
                player.transform.SetParent(playerRespawer.transform);

                PlayerScript scriptJugador = playerPrefab.GetComponent<PlayerScript>();
                scriptJugador.SelectController(i);

                playerRespawer.transform.Translate(new Vector2(coor[numCoor], 0));
                numCoor++;
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
}
