﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersSelector : MonoBehaviour
{
    public GameObject txtEmpezar;
    public Sprite[] skins;
    private bool listos = false;
    private Dictionary<int, int> mandoYPanel = new Dictionary<int, int>();
    private Dictionary<int, int> mandoYSkin = new Dictionary<int, int>();
    private Queue<int> skinsDisponibles = new Queue<int>();
    private Stack<int> paneles = new Stack<int>();
    private PlayerInputs input;
    private Coroutine corrutina;
    [SerializeField] private int tiempoEspera = 2;
    [SerializeField] private GameObject menu;
    [SerializeField] private MenuScript menuScript;

    private void Awake()
    {
        //Inicialización del Input
        /*input = new PlayerInputs();

        input.Menu.Asignar.performed += ctxAsignar => asignarJugador(ctxAsignar);
        input.Menu.Desasignar.performed += ctxDesasignar => desasignarJugador(ctxDesasignar);
        input.Menu.Desasignar.canceled += ctxSoltarBoton => soltarBoton();
        input.Menu.CambiarSkin.performed += ctxCambiar => cambiarSkin(ctxCambiar);
        input.Menu.Empezar.performed += ctxEmpezar => empezarJuego();

        //Inicialización del array de los paneles
        paneles.Push(3);
        paneles.Push(2);
        paneles.Push(1);
        paneles.Push(0);

        //Inicialización del array de las Skins
        skinsDisponibles.Enqueue(0);
        skinsDisponibles.Enqueue(1);
        skinsDisponibles.Enqueue(2);
        skinsDisponibles.Enqueue(3);

        //Activar input
        input.Enable();

        Debug.Log("NO DEBO MOSTRARME");*/
    }

    private void Start()
    {
        //Inicialización del Input
        input = new PlayerInputs();

        input.Menu.Asignar.performed += ctxAsignar => asignarJugador(ctxAsignar);
        input.Menu.Desasignar.performed += ctxDesasignar => desasignarJugador(ctxDesasignar);
        input.Menu.Desasignar.canceled += ctxSoltarBoton => soltarBoton();
        input.Menu.CambiarSkin.performed += ctxCambiar => cambiarSkin(ctxCambiar);
        input.Menu.Empezar.performed += ctxEmpezar => empezarJuego();

        //Inicialización del array de los paneles
        paneles.Push(3);
        paneles.Push(2);
        paneles.Push(1);
        paneles.Push(0);

        //Inicialización del array de las Skins
        skinsDisponibles.Enqueue(0);
        skinsDisponibles.Enqueue(1);
        skinsDisponibles.Enqueue(2);
        skinsDisponibles.Enqueue(3);

        //Activar input
        input.Enable();

        Debug.Log("NO DEBO MOSTRARME");
    }

    private void OnEnable()
    {
        input.Enable();
        acualizarAviso();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    /// <summary>
    /// Método para asignar un nuevo mando
    /// </summary>
    /// <param name="ctx">Variable generada al pulsar un botón del mando para identificarlo</param>
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
                mandoYPanel.Add(-1, numPanel);

                Transform miPanel = transform.GetChild(numPanel);
                GameObject aux = miPanel.gameObject;
                aux.SetActive(true);

                //miPanel.GetChild(1).GetComponent<Image>().color = getColorInInt(skinsDisponibles.Peek());
                miPanel.GetChild(1).GetComponent<Image>().sprite = getSpriteInInt(skinsDisponibles.Peek());

                skinsDisponibles.Dequeue();
                paneles.Pop();
                //Debug.Log("INFO: Se ha asignado el teclado correctamente");
            }
            catch (ArgumentException)
            {
                //Debug.Log("ERROR: Ya está asignado el teclado");
            }
        }
        else
        {//Es un mando
            try
            {
                int numPanel = paneles.Peek();
                mandoYPanel.Add(GetGamepadArrayPosition(id), numPanel);

                Transform miPanel = transform.GetChild(numPanel);

                GameObject aux = miPanel.gameObject;
                aux.SetActive(true);

                //miPanel.GetChild(1).GetComponent<Image>().color = getColorByInt(skinsDisponibles.Peek());
                miPanel.GetChild(1).GetComponent<Image>().sprite = getSpriteInInt(skinsDisponibles.Peek());

                skinsDisponibles.Dequeue();
                paneles.Pop();

                //Debug.Log("INFO: Se ha asignado el mando correctamente");
            }
            catch (ArgumentException)
            {
                //Debug.Log("ERROR: Ya está asignado el mando");
            }
        }

        acualizarAviso();

    }
    
    /// <summary>
    /// Método para desasignar un mando ya asignado
    /// </summary>
    /// <param name="ctx">Variable generada al pulsar un botón del mando para identificarlo</param>
    private void desasignarJugador(InputAction.CallbackContext ctx)
    {
        int key = GetGamepadArrayPosition(ctx.control.device.deviceId);
        
        if (mandoYPanel.ContainsKey(key))
        {
            foreach (KeyValuePair<int, int> control in mandoYPanel)
            {
                if (control.Key.Equals(key))
                {
                    Transform miPanel = transform.GetChild(control.Value);

                    GameObject aux = miPanel.gameObject;
                    aux.SetActive(false);

                    skinsDisponibles.Enqueue(getIntBySprite(miPanel.GetChild(1).GetComponent<Image>().sprite));

                    paneles.Push(control.Value);
                    mandoYPanel.Remove(key);
                    break;
                }
            }

            acualizarAviso();
        }
        else
        {
            corrutina = StartCoroutine(cerrarSelector(tiempoEspera));
        }
    }

    private void cambiarSkin(InputAction.CallbackContext ctx)
    {
        int key = GetGamepadArrayPosition(ctx.control.device.deviceId);

        if (mandoYPanel.ContainsKey(key))
        {
            foreach (KeyValuePair<int, int> control in mandoYPanel)
            {

                if (control.Key.Equals(key))
                {

                    Transform miPanel = transform.GetChild(control.Value);

                    int aux = getIntBySprite(miPanel.GetChild(1).GetComponent<Image>().sprite);
                    miPanel.GetChild(1).GetComponent<Image>().sprite = getSpriteInInt(skinsDisponibles.Dequeue());
                    skinsDisponibles.Enqueue(aux);
                }

            }
        }
        else
        {
            Debug.Log("ERROR: Este controlador no se encuentra actualmente asignado");
        }
    }

    public void empezarJuego()
    {
        if (listos)
        {
            int miSkin = -1;

            foreach (KeyValuePair<int, int> control in mandoYPanel)
            {
                
                miSkin = getIntBySprite(transform.GetChild(control.Value).GetChild(1).GetComponent<Image>().sprite);

                PlayerContainer.ayadirControler(control.Key, miSkin);
            }
            menuScript.Jugar();
        }
        
    }

    private void acualizarAviso()
    {
        if (paneles.Count < 3)
        {
            actualizarTexto("empezar");
            listos = true;
        }
        else
        {
            actualizarTexto("unirse");
            listos = false;
        }
    }

    private void actualizarTexto(string clave)
    {
        txtEmpezar.GetComponent<SeleccionarTexto>().id = clave;
        txtEmpezar.GetComponent<SeleccionarTexto>().modificarTexto();
    }

    private void soltarBoton()
    {
        if (corrutina != null)
        {
            Debug.Log(">>>INFO: Se ha detenido la corrutina: " + corrutina);
            StopCoroutine(corrutina);
            corrutina = null;
            acualizarAviso();
        }
        
    }

    private Color getColorByInt(int numSkin)
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
    
    private int getIntByColor(Color miColor)
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

    private Sprite getSpriteInInt(int numSkin)
    {
        return skins[numSkin];
    }

    private int getIntBySprite(Sprite miSprite)
    {
        int aux = -1;

        if (miSprite.name.Equals("Robot1(NoFondo)"))
        {
            aux = 0;
        }

        if (miSprite.name.Equals("Robot2(NoFondo)"))
        {
            aux = 1;
        }

        if (miSprite.name.Equals("Robot3(NoFondo)"))
        {
            aux = 2;
        }

        if (miSprite.name.Equals("Robot4(NoFondo)"))
        {
            aux = 3;
        }

        return aux;

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

    IEnumerator cerrarSelector(int tiempo)
    {
        //Debug.Log(">>>INFO: Se ha iniciado la corrutina correctamente");
        
        while (true)
        {

            if (tiempo < 0)
            {
                menu.SetActive(true);
                menuScript.cerrarSelector();

                this.gameObject.SetActive(false);
            }

            txtEmpezar.GetComponent<SeleccionarTexto>().id = "volviendo";
            txtEmpezar.GetComponent<SeleccionarTexto>().modificarTexto((tiempo + 1).ToString());

            yield return new WaitForSeconds(1);
            
            tiempo--;
        }
        
    }
}