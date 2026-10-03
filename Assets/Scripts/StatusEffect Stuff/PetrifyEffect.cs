using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/Petrify")]
public class PetrifyEffect : StatusEffect
{
    private int petrifyStage;
    
    private readonly int petrifyEnd = 3;
    
    public override void DoTurnStart(CharacterBehavior owner)
    {
        petrifyStage++;
        
        if (petrifyStage >= 3)
        {
            owner.TakeDamage(owner.Stats.maxHealth * 7);
        }
    }
}
