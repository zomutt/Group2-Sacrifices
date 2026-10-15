using System.Collections;
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
    
    private bool allowSliderChange = false;

    void OnEnable()
    {
        StartCoroutine(SetSliderValues());
    }
    
    private IEnumerator SetSliderValues()
    {
        while (brightness == null) yield return null;
        
        brightness.value = StoredSettings.exposure.Percentage;
        musicVol.value = StoredSettings.musicVol.Percentage;
        sfxVol.value = StoredSettings.sfxVol.Percentage;
        
        allowSliderChange = true;
    }

    public void ReadSliderValues()
    {
        if (!allowSliderChange) return; // To prevent a bug where sliders reset settings when first enabled
        
        StoredSettings.exposure.SetPercentage(brightness.value);
        StoredSettings.musicVol.SetPercentage(musicVol.value);
        StoredSettings.sfxVol.SetPercentage(sfxVol.value);
        
        Debug.Log($"changed value {StoredSettings.musicVol.Percentage} {StoredSettings.sfxVol.Percentage} {StoredSettings.exposure.Percentage}");
    }
}
