using TMPro;
using UnityEngine;

public class WinScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    private readonly string[] difficultyNames = new string[3]
    {
        "Cheat",
        "Normal",
        "Hard"
    };
    
    void OnEnable()
    {
        scoreText.text = $"{difficultyNames[StoredSettings.difficulty]} Mode Score: {StoredSettings.score}";
    }
}
