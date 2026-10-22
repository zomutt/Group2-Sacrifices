using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StoredSettings : MonoBehaviour
{
    public class SliderSetting
    {
        private float value;
        
        private float minVal;
        private float maxVal;
        private float difference;
        
        public float Value => value;
        public float Percentage => (value - minVal) / difference;
        
        public UnityEvent valueChange = new UnityEvent();
        
        public void SetValue(float newValue)
        {
            value = Mathf.Clamp(newValue, minVal, maxVal);
            
            valueChange.Invoke();
        }
        
        public void SetPercentage(float newPercentage)
        {
            value = Mathf.Clamp(newPercentage * difference + minVal, minVal, maxVal);
            
            valueChange.Invoke();
        }
        
        public SliderSetting(float startValue, float min, float max)
        {
            minVal = min;
            maxVal = max;
            
            difference = max - min;
            
            SetValue(startValue);
        }
    }
    
    public static SliderSetting exposure = new SliderSetting(0, 0, 2);
    
    public static SliderSetting musicVol = new SliderSetting(0.5f, 0, 1);
    public static SliderSetting sfxVol = new SliderSetting(0.5f, 0, 1);
    
    public static int difficulty = 1;
    public static readonly float[] difficultyHealthMult =
    {
        10,
        1.25f,
        0.8f
    };
    
    public static int score;
    
    public static int playCount;
}
