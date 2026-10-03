using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/Strength")]
public class StrengthEffect : StatusEffect
{
    public override void DoTurnStart(CharacterBehavior owner)
    {
        owner.addedStrength += power;
    }
}
