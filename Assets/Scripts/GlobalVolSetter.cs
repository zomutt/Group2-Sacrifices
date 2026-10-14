using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolSetter : MonoBehaviour
{
    [SerializeField]
    private Volume volume;
    
    ColorAdjustments colorAdjustments;
    
    private void Awake()
    {
        if (!volume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogError("No color adjustments override found");
            return;
        }
        
        SetExposure();
    }
    
    private void SetExposure()
    {
        colorAdjustments.postExposure.value = StoredSettings.exposure.Value;
    }
    
    void OnEnable() {StoredSettings.exposure.valueChange.AddListener(SettingChangeEvent);}
    void OnDisable() {StoredSettings.exposure.valueChange.RemoveListener(SettingChangeEvent);}
    
    private void SettingChangeEvent()
    {
        SetExposure();
    }
}
