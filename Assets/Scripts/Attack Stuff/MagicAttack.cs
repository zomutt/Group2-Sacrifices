using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/MagicAttack")]
public class MagicAttack : PlainAttack
{
    protected override int GetAttackPower(CharacterBehavior chara, float basePower)
    {
        return (int)(GetMagicPowerOf(chara) * basePower);
    }
}
