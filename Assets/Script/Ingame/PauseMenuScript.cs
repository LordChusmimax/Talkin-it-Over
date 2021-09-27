using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript current;
    
    void Start()
    {
        current = this;
        gameObject.SetActive(false);
        OtherEvents();
    }

    private void OtherEvents()
    {
        GameEvents.current.PausePressed += OnPause;
    }

    private void OnPause(bool paused)
    {
        gameObject.SetActive(paused);
    }

    public void Resume()
    {
        if (RoundValues.paused)
        {
            GameEvents.current.PressPause();
        }
    }

    public void Leave()
    {
        PlayerContainer.clearArray();
        GameObject roundSystem = GameObject.Find("ContainerRoundSystem");
        Destroy(roundSystem);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
