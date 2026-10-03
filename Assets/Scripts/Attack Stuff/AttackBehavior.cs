using System;
using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    public string attackName;
    
    [SerializeField]
    protected float power;

    [SerializeField]
    private int hitParticleIndex = -1;

    [SerializeField]
    private AudioClip attackSound;
    
    [Serializable]
    public class TargetTyping
    {
        public bool targetsEnemies = true;
        public bool hitsAll = false;
    }
    public TargetTyping targetType;

    public abstract void MoveOnTarget(CharacterBehavior from, CharacterBehavior to);

    protected void OnHitSpecialFX(CharacterBehavior target)
    {
        if (hitParticleIndex >= 0) ParticleOnTarget(hitParticleIndex, target);
        
        if (attackSound != null) PlaySFX(attackSound);
    }

    private void ParticleOnTarget(int index, CharacterBehavior target)
    {
        // do stuff to summon a particle on the target
        // from the particle pooler's instance
    }

    private void PlaySFX(AudioClip sound)
    {
        AudioManager.Instance.PlaySFX(sound);
    }
    
    protected int GetMagicPowerOf(CharacterBehavior chara)
    {
        if (chara.isEnemy)
            return chara.Stats.magic;
        else
            return PartyController.Instance.GetPartyTotalMagic();
    }
}
