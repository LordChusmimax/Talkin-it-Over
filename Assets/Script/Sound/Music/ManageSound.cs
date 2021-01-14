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
    private Scene scene;
    private string lastScene;
    private bool songStarted = false;
    private static ManageSound current;
    [SerializeField] private float musicVolume = 0.25f;

    void Awake()

    {
        if (current != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            lastScene = "Menu";
            current = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            song = GetComponents<AudioSource>()[0];
            sound = GetComponents<AudioSource>()[1];
            StartSong();
        }
    }

    private void Update()
    {
        if (!song.isPlaying)
        {
            if (scene.name == "Lab" || scene.name.StartsWith("Stage"))
            {
                int songNumber = Random.Range(0, songListBattle.Length);
                song.clip = songListBattle[songNumber];
                StartSong();
            }
            else if (scene.name == "Menu")
            {
                int songNumber = Random.Range(0, songListMenu.Length);
                song.clip = songListMenu[songNumber];
                StartSong();
            }
        }
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
        this.scene = scene;
        if (scene.name == "Lab" || scene.name.StartsWith("Stage"))
        {
            if (lastScene == "Menu")
            {
                lastScene = "Scene";
                int songNumber = Random.Range(0, songListBattle.Length);
                song.clip = songListBattle[songNumber];
                StartSong();
            }
        }
        else if (scene.name == "Menu")
        {
            lastScene = "Menu";
            song.clip = songListMenu[0];
            StartSong();
        }
    }

    public void SelectItem()
    {
        if (sound != null)
        {
            sound.clip = selectedItem;
            sound.Play();
        }
    }

    public void ClickItem()
    {
        sound.clip = clickedItem;
        sound.Play();
    }

}
