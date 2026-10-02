using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/HealPerTurn")]
public class HealPerTurnStatus : StatusEffect
{
    [SerializeField]
    private int healPower = 5;
    
    public override void DoTurnStart(CharacterBehavior owner)
    {
        owner.GainHealth(healPower);
    }
}
