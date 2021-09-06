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
    public AudioClip[] songListCritical;
    public AudioClip[] songListIdle;
    public AudioClip[][] songListList;
    public AudioClip selectedItem;
    public AudioClip clickedItem;
    private Scene scene;
    private string lastScene;
    private static ManageSound current;
    private int songVersion = 1;
    private int songNumber;
    [SerializeField] private float musicVolume = 0.25f;

    void Awake()

    {
        songListList = new AudioClip[][] { songListMenu, songListBattle, songListCritical, songListIdle };
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
                songNumber = Random.Range(0, songListBattle.Length);
                song.clip = songListList[songVersion][songNumber];
                StartSong();
            }
            else if (scene.name == "Menu")
            {
                songNumber = Random.Range(0, songListMenu.Length);
                song.clip = songListMenu[songNumber];
                StartSong();
            }
        }
        /*
        if (Time.frameCount%600 == 0)
        {
           songVersion = Random.Range(1, 4);
           Debug.Log(songVersion);
            var time = song.time;
            song.clip = songListList[songVersion][songNumber];
            song.time = time;
        }
        */
    }

    public void OnPause(bool pause)
    {
        var time = song.time;
        song.clip = songListList[pause ? 3 : songVersion][songNumber];
        Debug.Log(song.clip.name);
        song.time = time;
        song.Play();
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

            GameEvents.current.PausePressed += OnPause;
            if (lastScene == "Menu")
            {
                int songNumber = Random.Range(0, songListBattle.Length);
                song.clip = songListList[songVersion][songNumber];
                song.time = 0f;
                StartSong();
                lastScene = "Scene";
            }
        }
        else if (scene.name == "Menu")
        {
            song.clip = songListMenu[0];
            song.time = 0f;
            StartSong();
            lastScene = "Menu";
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
