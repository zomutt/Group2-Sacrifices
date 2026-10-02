using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/HealMove")]
public class HealMove : AttackBehavior
{
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        to.GainHealth(GetHealPower(from, power));

        OnHitSpecialFX(to);
    }
    
    private int GetHealPower(CharacterBehavior chara, float basePower)
    {
        return (int)(GetMagicPowerOf(chara) * basePower);
    }
}
