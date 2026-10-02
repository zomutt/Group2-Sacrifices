using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/PlainAttack")]
public class PlainAttack : AttackBehavior
{

    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        to.TakeDamage(GetAttackPower(from.Stats, power));

        OnHitSpecialFX(to);
    }

    private int GetAttackPower(CharacterStats stats, float basePower)
    {
        return (int)(stats.strength * basePower);
    }
}
