using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    
    private float levelTime;
    
    void Awake()
    {
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
    }
    
    void Start()
    {
        levelTime = 180f;
    }
    
    void Update()
    {
        levelTime -= Time.deltaTime;
        
        timerText.text = Mathf.CeilToInt(levelTime / 2f).ToString();
    }
}
