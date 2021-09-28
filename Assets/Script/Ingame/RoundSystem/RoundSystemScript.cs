using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundSystemScript : MonoBehaviour
{

    private static RoundSystemScript current;



    private void Awake()
    {
        current = this;
    }

    public void deletedPlayer(int idPlayer)
    {
        nextRound();
        
    }

    public void nextRound()
    {
        int escena = Random.Range(1, 6);
        SceneManager.LoadScene("Stage" + escena);
    }
}
