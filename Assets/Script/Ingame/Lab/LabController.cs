using UnityEngine;
using UnityEngine.SceneManagement;

public class LabController : MonoBehaviour
{
    private PlayerInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new PlayerInputs();
        inputs.Player.ReloadLab.performed += ctxSpecial => reloadLab();
        inputs.Player.CloseLab.performed += ctxSpecial => closeLab();
        inputs.Enable();

        Debug.Log(">>>INFO: Activado Lab\nPulse 'P' para recargar la escena\n" +
            "Pulse 'O' para cerrar el editor");
    }

    private void reloadLab()
    {
        SceneManager.LoadScene("Lab");
    }

    private void closeLab()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
