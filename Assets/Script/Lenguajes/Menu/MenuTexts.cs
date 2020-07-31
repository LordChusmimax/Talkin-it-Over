using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public static class MenuTexts
{
    public static string gameTitle {
        get
        {
            switch (Language)
            {
                case 0:
                    return "Da Game";
                case 1:
                    return "Er Juego";
            }
            return "";
        }
    }
}
