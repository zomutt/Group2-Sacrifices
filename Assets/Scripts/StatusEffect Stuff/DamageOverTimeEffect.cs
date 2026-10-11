using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/DamageOverTime")]
public class DamageOverTimeEffect : StatusEffect
{
    [SerializeField]
    private float damageTickLength;
    private float damageTickTimer;
    
    public override void TickStatusEffect(CharacterBehavior owner)
    {
        base.TickStatusEffect(owner);
        
        damageTickTimer += Time.deltaTime;
        
        if (damageTickTimer >= damageTickLength)
        {
            owner.TakeDamage(power, false);
            damageTickTimer -= damageTickLength;
        }
    }
}
