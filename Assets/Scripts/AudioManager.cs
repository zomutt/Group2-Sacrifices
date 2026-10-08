using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip music;

    [Header("Volume")]
    [SerializeField] private float musVolume = 1;
    [SerializeField] private float sfxVolume = 1;

    private RandomMusic caveMusic;
    private bool musicStopped;

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

        caveMusic = GetComponentInChildren<RandomMusic>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Fades the cave music out; it comes back on its own when the next scene loads
    public void StopMusic()
    {
        if (!caveMusic) return;
        caveMusic.StopMusic();
        musicStopped = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!musicStopped || !caveMusic) return;
        musicStopped = false;
        caveMusic.StartMusic();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip && audioSource) audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySFX(AudioClip clip)
    {
        PlaySFX(clip, sfxVolume);
    }
}
