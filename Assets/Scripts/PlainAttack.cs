using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/PlainAttack")]
public class PlainAttack : AttackBehavior
{
    [SerializeField]
    private AudioClip attackSound;

    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(attackSound);

        to.TakeDamage(GetAttackPower(from.Stats, power));
    }

    private int GetAttackPower(CharacterStats stats, int basePower)
    {
        return stats.strength * basePower;
    }
}
