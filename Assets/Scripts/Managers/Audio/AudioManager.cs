using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public Sound[] sounds;

    public static AudioManager instance;
    

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeSounds();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += PlayTitleMusic;
        SceneManager.sceneLoaded += PlayLevelMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= PlayTitleMusic;
        SceneManager.sceneLoaded -= PlayLevelMusic;
    }

    private void Update()
    {
        
    }

    private void InitializeSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void PlayClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null)
            return;

        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void StopClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null) return;

        s.source.Stop();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null) return;

        s.source.volume = volume;

    }

    public void SetVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }

    public float GetVolume()
    {
        return sounds[0].source.volume;
    }
    public void SetOverallVolume()
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume;
        }
    }
    public void FadeOutAudio(string name, float fadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        StartCoroutine(AudioFadeOut.FadeOut(s.source, fadeTime));
    }

    private void PlayTitleMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0 && !sounds[3].source.isPlaying)
        {
            PlayClip("Title");
            StopClip("Level");
            StopClip("Boss");
        }
       
    }

    private void PlayLevelMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 2 && !sounds[0].source.isPlaying)
        {
            PlayClip("Level");
            sounds[0].source.volume = 1f;
        }
    }
}
