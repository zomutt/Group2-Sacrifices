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
    private AudioClip[] attackSounds;
    
    [Serializable]
    public class TargetTyping
    {
        public bool targetsEnemies = true;
        public bool targetsSelf = false;
    }
    public TargetTyping targetType;

    public abstract void MoveOnTarget(CharacterBehavior from, CharacterBehavior to);

    protected void OnHitSpecialFX(CharacterBehavior target)
    {
        if (hitParticleIndex >= 0) ParticleOnTarget(hitParticleIndex, target);

        if (attackSounds != null)    // Array added
        {
            int sfx = UnityEngine.Random.Range(0, attackSounds.Length);
            AudioManager.Instance.PlaySFX(attackSounds[sfx]);
        }
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
    
    protected int GetArmorOf(CharacterBehavior target)
    {
        return target.Stats.armor + target.addedArmor;
    }
}
