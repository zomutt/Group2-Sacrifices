using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/PlainAttack")]
public class PlainAttack : AttackBehavior
{
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        to.TakeDamage(GetAttackPower(from.Stats, power));
    }

    private int GetAttackPower(CharacterStats stats, int basePower)
    {
        return 5; // Not implemented yet
    }
}
