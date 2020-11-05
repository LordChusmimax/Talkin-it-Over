using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageSound : MonoBehaviour
{

    private AudioSource song;
    public AudioClip[] songList;
    private bool songStarted = false;
    private static ManageSound current;
    [SerializeField] private float musicVolume = 0.25f;

    void Start()

    {
        if (current != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        song = GetComponent<AudioSource>();
        StartSong();
    }

    public void EndSong()
    {
        song.Stop();
    }

    public void StartSong()
    {
        song.Play();
        song.volume = musicVolume;
    }

    public void PauseSong()
    {
        song.Pause();
    }

    public void ResumeSong()
    {
        song.UnPause();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lab")
        {
            EndSong();
            song.clip = songList[1];
            StartSong();
        }
    }
}
