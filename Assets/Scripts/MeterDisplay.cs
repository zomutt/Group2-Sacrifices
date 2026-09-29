using UnityEngine;

public class MeterDisplay : MonoBehaviour
{
    private float defaultMeterWidth;

    private RectTransform meter;

    private float currentValue;
    private float maxValue;

    private void Awake()
    {
        meter = transform.GetChild(0).GetComponent<RectTransform>();
        defaultMeterWidth = meter.rect.width;
    }

    private void UpdateMeter()
    {
        meter.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            Mathf.Clamp01(currentValue / maxValue) * defaultMeterWidth);
    }

    public void SetMeterValueAndMax(float newValue, float newMax)
    {
        currentValue = newValue;
        maxValue = newMax;
        UpdateMeter();
    }

    public void SetMeterValue(float newValue)
    {
        currentValue = newValue;
        UpdateMeter();
    }
}
