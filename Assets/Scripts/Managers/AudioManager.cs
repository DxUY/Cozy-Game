using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene detection

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
    }

    public List<Sound> sounds;
    public List<Sound> musicTracks;
    public MusicData musicData; // 🎵 Reference to Scene & Area Music

    private Dictionary<string, AudioClip> soundDictionary;
    private Dictionary<string, AudioClip> musicDictionary;
    private Dictionary<string, string> sceneMusicMapping;
    private Dictionary<string, string> areaMusicMapping;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private string currentSceneMusic;
    private string currentAreaMusic;

    private void Awake()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        soundDictionary = new Dictionary<string, AudioClip>();
        musicDictionary = new Dictionary<string, AudioClip>();
        sceneMusicMapping = new Dictionary<string, string>();
        areaMusicMapping = new Dictionary<string, string>();

        foreach (var sound in sounds)
            soundDictionary[sound.name] = sound.clip;

        foreach (var track in musicTracks)
            musicDictionary[track.name] = track.clip;

        if (musicData != null)
        {
            foreach (var entry in musicData.sceneMusicEntries)
                sceneMusicMapping[entry.identifier] = entry.track;

            foreach (var entry in musicData.areaMusicEntries)
                areaMusicMapping[entry.identifier] = entry.track;
        }

        // 🔥 Automatically detect & play first scene's music
        string currentScene = SceneManager.GetActiveScene().name;
        HandleSceneLoaded(currentScene);
    }

    private void OnEnable()
    {
        EventBus.OnPlaySound += PlaySound;
        EventBus.OnPlayMusic += PlayMusic;
        EventBus.OnStopMusic += StopMusic;
        EventBus.OnSceneLoaded += HandleSceneLoaded;
        EventBus.OnAreaMusicChange += ChangeAreaMusic;
    }

    private void OnDisable()
    {
        EventBus.OnPlaySound -= PlaySound;
        EventBus.OnPlayMusic -= PlayMusic;
        EventBus.OnStopMusic -= StopMusic;
        EventBus.OnSceneLoaded -= HandleSceneLoaded;
        EventBus.OnAreaMusicChange -= ChangeAreaMusic;
    }

    private void HandleSceneLoaded(string sceneName)
    {
        if (sceneMusicMapping.TryGetValue(sceneName, out string musicName))
        {
            currentSceneMusic = musicName;
            PlayMusic(musicName);
        }
    }

    public void ChangeAreaMusic(string areaName)
    {
        if (areaMusicMapping.TryGetValue(areaName, out string musicName))
        {
            currentAreaMusic = musicName;
            PlayMusic(musicName);
        }
        else
        {
            PlayMusic(currentSceneMusic); // 🔄 If no area music, play scene music
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }

    public void PlayMusic(string musicName)
    {
        if (musicDictionary.TryGetValue(musicName, out AudioClip clip))
        {
            if (musicSource.clip == clip) return; // 🚀 Avoid restarting the same track

            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music '{musicName}' not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
