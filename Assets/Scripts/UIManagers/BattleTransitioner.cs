using UnityEngine;
using UnityEngine.Events;

public class BattleTransitioner : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    
    public enum ScreenTransitType
    {
        Clear,
        CircleIn,
        CircleOut
    }
    
    private ScreenTransitType outType;
    
    private GameState finalState;
    
    public UnityEvent transitionInEnd;
    
    public void StartTransition(ScreenTransitType type1, ScreenTransitType type2)
    {
        LevelManager.Cutscene = true;
        
        anim.SetTrigger(type1.ToString());
        
        outType = type2;
    }
    
    public void EndClosingTransition()
    {
        anim.SetTrigger(outType.ToString());
        
        transitionInEnd.Invoke();
        transitionInEnd.RemoveAllListeners();
    }
    
    public void EndOpeningTransition()
    {
        LevelManager.Cutscene = false;
    }
}
