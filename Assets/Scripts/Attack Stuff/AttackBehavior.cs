using System;
using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    public string attackName;
    public Sprite attackIcon;
    
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

    protected virtual void OnHitSpecialFX(CharacterBehavior target)
    {
        if (hitParticleIndex >= 0) ParticleOnTarget(hitParticleIndex, target);

        if (attackSounds != null && attackSounds.Length > 0)    // Array added
        {
            int sfx = UnityEngine.Random.Range(0, attackSounds.Length);
            AudioManager.Instance.PlaySFX(attackSounds[sfx]);
        }
    }

    private void ParticleOnTarget(int index, CharacterBehavior target)
    {
        GameObject gameObject = ParticleManager.Instance.GetParticleObject(index);
        
        gameObject.transform.position = target.transform.position;
        gameObject.SetActive(true);
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
