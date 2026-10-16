using TMPro;
using UnityEngine;

public class WinScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    void OnEnable()
    {
        scoreText.text = $"Score: {StoredSettings.score}";
    }
}
