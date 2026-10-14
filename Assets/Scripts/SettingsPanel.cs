using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Slider brightness;
    [SerializeField]
    private Slider musicVol;
    [SerializeField]
    private Slider sfxVol;

    void Start()
    {
        SetSliderValues();
    }
    
    private void SetSliderValues()
    {
        brightness.value = StoredSettings.exposure.Percentage;
        musicVol.value = StoredSettings.musicVol.Percentage;
        sfxVol.value = StoredSettings.sfxVol.Percentage;
    }

    public void ReadSliderValues()
    {
        StoredSettings.exposure.SetPercentage(brightness.value);
        StoredSettings.musicVol.SetPercentage(musicVol.value);
        StoredSettings.sfxVol.SetPercentage(sfxVol.value);
    }
}
