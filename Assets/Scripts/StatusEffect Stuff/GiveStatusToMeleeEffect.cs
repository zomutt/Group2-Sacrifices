using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/StatusEffects/GiveStatusToMelee")]
public class GiveStatusToMeleeEffect : StatusEffect
{
    [SerializeField]
    private StatusEffect giveOnHurt;
    
    [SerializeField]
    private int particleIndex;
    
    public override void DoOnHurtMelee(CharacterBehavior owner, CharacterBehavior attacker)
    {
        Debug.Log($"Giving status {giveOnHurt.name} to {attacker.name}");
        
        StatusEffect newStatusEffect = Instantiate(giveOnHurt);
        
        if (particleIndex > -1) ParticleOnTarget(particleIndex, attacker);
        
        attacker.AddStatusEffect(newStatusEffect);
    }

    private void ParticleOnTarget(int index, CharacterBehavior target)
    {
        GameObject gameObject = ParticleManager.Instance.GetParticleObject(index);
        
        if (gameObject == null)
        {
            Debug.Log($"Particle failsafe activated for reflective status with particle index {index}");
            return;
        }
        
        gameObject.transform.position = target.transform.position;
        gameObject.SetActive(true);
    }
}
