using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/GiveStatusToMelee")]
public class GiveStatusToMeleeEffect : StatusEffect
{
    [SerializeField]
    private StatusEffect giveOnHurt;
    
    public override void DoOnHurtMelee(CharacterBehavior owner, CharacterBehavior attacker)
    {
        StatusEffect newStatusEffect = Instantiate(giveOnHurt);
        
        attacker.statuses.Add(newStatusEffect);
    }
}
