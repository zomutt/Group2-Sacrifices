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
    [SerializeField] private float sfxVolume = .5f;

    private RandomMusic caveMusic;

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

    // Cave music plays continuously and is never stopped
    public void StopMusic() { }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (!clip || !audioSource) return;
        audioSource.Stop();    // Prevent audio spam. It's not best, but it's better.
        audioSource.PlayOneShot(clip, volume * sfxVolume);
    }
}
