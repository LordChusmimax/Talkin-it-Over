﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoundValues;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
        pausePressed = null;
    }

    public event Action<bool> pausePressed;
    public void PressPause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pausePressed(paused);
    }

}
