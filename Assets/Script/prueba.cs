using System.Collections;
using System;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Sistema: " + System.Environment.OSVersion.ToString());
        Debug.Log("Máquina: " + System.Environment.MachineName.ToString());
        Debug.Log("Usuario: " + System.Environment.UserName.ToString());
        Debug.Log("Num procesadores: " + System.Environment.ProcessorCount.ToString());

        System.Console.WriteLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
