using UnityEngine;
using UnityEngine.UI;

public class CharbarFlasher : MonoBehaviour
{
    private Slider slider;
    
    [SerializeField]
    private Image sprite;
    
    private float timer;
    private readonly float timerRate = 30;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        UpdateFlash();
    }
    
    private void UpdateFlash()
    {
        // Increase timer at a certain rate
        timer = (timer + GetTimerRate()) % 360;
        
        sprite.color = new Color(1, 1, 1, GetAlphaForFlash(timer));
    }
    
    private float GetTimerRate()
    {
        return Time.deltaTime * slider.value * timerRate;
    }
    
    private float GetAlphaForFlash(float time)
    {
        float flashAlpha = (Mathf.Sin(time) + 1) / 2f;
        flashAlpha *= Mathf.Lerp(-1, 1, slider.value);
        
        return Mathf.Clamp01(flashAlpha);
    }
}
