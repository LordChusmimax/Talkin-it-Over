using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        OtherEvents();
    }

    private void OtherEvents()
    {
        GameEvents.current.pausePressed += OnPause;
    }

    private void OnPause(bool paused)
    {
        gameObject.SetActive(paused);
    }

    public void Continue()
    {
        GameEvents.current.PressPause();
    }

    public void Leave()
    {
        SceneManager.LoadScene(0);
    }
}
