using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageSound : MonoBehaviour
{

    private AudioSource song;
    private AudioSource sound;
    public AudioClip[] songListMenu;
    public AudioClip[] songListBattle;
    public AudioClip selectedItem;
    public AudioClip clickedItem;
    private bool songStarted = false;
    private static ManageSound current;
    [SerializeField] private float musicVolume = 0.25f;

    void Start()

    {
        if (current != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        song = GetComponents<AudioSource>()[0];
        sound = GetComponents<AudioSource>()[1];
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
        if (scene.name == "Lab" || scene.name.StartsWith("Stage"))
        {
            EndSong();
            int songNumber = Random.Range(0, songListBattle.Length);
            song.clip = songListBattle[songNumber];
            StartSong();
        }
        else if (scene.name == "Menu")
        {   
            song.clip = songListMenu[0];
            StartSong();
        }
    }

    public void SelectItem()
    {
        sound.clip = selectedItem;
        sound.Play();
    }

    public void ClickItem()
    {
        sound.clip = clickedItem;
        sound.Play();
    }
}
