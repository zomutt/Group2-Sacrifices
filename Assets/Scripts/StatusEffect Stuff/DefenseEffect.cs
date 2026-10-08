using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/Defense")]
public class DefenseEffect : StatusEffect
{
    public override void DoOnAdd(CharacterBehavior owner)
    {
        owner.addedArmor += power;
    }

    public override void DoOnEnd(CharacterBehavior owner)
    {
        owner.addedArmor -= power;
    }
}
