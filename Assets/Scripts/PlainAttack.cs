using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/PlainAttack")]
public class PlainAttack : AttackBehavior
{

    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        to.TakeDamage(GetAttackPower(from, power));

        OnHitSpecialFX(to);
    }

    private int GetAttackPower(CharacterBehavior chara, float basePower)
    {
        return (int)((chara.Stats.strength + chara.addedStrength) * basePower);
    }
}
