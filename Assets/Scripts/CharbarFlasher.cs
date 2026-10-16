using UnityEngine;
using UnityEngine.UI;

public class CharbarFlasher : MonoBehaviour
{
    private Slider slider;
    
    private SpriteRenderer sprite;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
}
