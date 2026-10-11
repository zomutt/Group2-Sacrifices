using UnityEngine;

public class Settings : MonoBehaviour
{   
    // TO-DO: Brightness

    public void SetSFXVolume(float v)
    {
        AudioManager.Instance.SetSFXVolume(v);
    }
    
    public void SetMusicVolume(float v)
    {
        AudioManager.Instance.SetMusicVolume(v);
    }
}
