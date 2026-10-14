using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    void Start()
    {
        LevelManager.LevelGameState = GameState.Cutscene;
    }

    public void EndOfCutscene()
    {
        LevelManager.LevelGameState = GameState.Exploring;
    }
}
