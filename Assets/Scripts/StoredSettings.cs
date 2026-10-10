using UnityEngine;
using UnityEngine.Events;

public class StoredSettings
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
        
        public void SetPercentage(float newPercentage)
        {
            value = Mathf.Clamp(newPercentage * difference + minVal, minVal, maxVal);
            
            valueChange.Invoke();
        }
        
        public SliderSetting(float startValue, float min, float max)
        {
            value = startValue;
            
            minVal = min;
            maxVal = max;
            
            difference = max - min;
        }
    }
    
    public static SliderSetting exposure = new SliderSetting(0, 0, 2);
}
