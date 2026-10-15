using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Volume")]
    [SerializeField] private float musicVolume = 1;
    [SerializeField] private float sfxVolume = .5f;

    [Header("Tracks")] 
    [SerializeField] private AudioClip[] musicTracks;

    private Dictionary<AudioClip, float> lastPlayed = new Dictionary<AudioClip, float>();
    private const float MinRepeatGap = 0.05f;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        musicSource.volume = musicVolume;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        StoredSettings.musicVol.valueChange.AddListener(UpdateMusicVolume);
        StoredSettings.sfxVol.valueChange.AddListener(UpdateSFXVolume);
        
        UpdateMusicVolume();
        UpdateSFXVolume();
    }
    
    private void OnDestroy()
    {
        if (Instance != this) return;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
        StoredSettings.musicVol.valueChange.RemoveListener(UpdateMusicVolume);
        StoredSettings.sfxVol.valueChange.RemoveListener(UpdateSFXVolume);
    }

    public void StartMusic()
    {
        StartCoroutine(PlayMusicSequence());
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void UpdateMusicVolume()
    {
        musicSource.volume = musicVolume * Mathf.Clamp01(StoredSettings.musicVol.Percentage);
    }

    public void UpdateSFXVolume()
    {
        sfxSource.volume = sfxVolume * Mathf.Clamp01(StoredSettings.sfxVol.Percentage);
    }

    private IEnumerator PlayMusicSequence()
    {
        foreach (AudioClip clip in musicTracks)
        {
            if (clip)
            {
                musicSource.clip = musicTracks[Random.Range(0, musicTracks.Length)];
                musicSource.Play();

                while (musicSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartMusic();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (!clip || !sfxSource) return;

        float last;
        if (lastPlayed.TryGetValue(clip, out last) && Time.unscaledTime - last < MinRepeatGap)
            return;

        lastPlayed[clip] = Time.unscaledTime;
        sfxSource.PlayOneShot(clip, volume * sfxVolume);
    }
}
