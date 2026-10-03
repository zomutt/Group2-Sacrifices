using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/HealPerTurn")]
public class HealPerTurnStatus : StatusEffect
{
    public override void DoTurnStart(CharacterBehavior owner)
    {
        owner.GainHealth(power);
    }
}
