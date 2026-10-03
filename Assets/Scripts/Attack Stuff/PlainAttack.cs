using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/PlainAttack")]
public class PlainAttack : AttackBehavior
{
    [SerializeField]
    protected bool isMelee;
    
    [SerializeField]
    protected bool usesSoulsForPower;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        if (power > 0)
            to.TakeDamage(GetAttackPower(from, power));

        OnHitSpecialFX(to);
        
        if (isMelee)
            to.DoOnHurtMelee(from);
    }

    protected int GetAttackPower(CharacterBehavior chara, float basePower)
    {
        int totalPower;
        
        if (usesSoulsForPower)
            totalPower = LevelManager.Instance.Souls / 10;
        else
            totalPower = (int)((chara.Stats.strength + chara.addedStrength) * basePower);
        
        return Mathf.Max(0, totalPower);
    }
}
