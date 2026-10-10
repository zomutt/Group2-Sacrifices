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
    }

    public void ReadSliderValues()
    {
        StoredSettings.exposure.SetPercentage(brightness.value);
    }
}
