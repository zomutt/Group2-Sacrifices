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
    }
    
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip && audioSource) audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySFX(AudioClip clip)
    {
        PlaySFX(clip, 1f);
    }
}
