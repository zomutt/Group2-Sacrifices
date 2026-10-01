using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    [SerializeField]
    protected int power;

    [SerializeField]
    private int hitParticleIndex = -1;

    [SerializeField]
    private AudioClip attackSound;

    public abstract void MoveOnTarget(CharacterBehavior from, CharacterBehavior to);

    protected void OnHitSpecialFX()
    {
        if (hitParticleIndex >= 0) ParticleOnTarget(hitParticleIndex);

        if (attackSound) PlaySFX(attackSound);
    }

    private void ParticleOnTarget(int index)
    {

    }

    private void PlaySFX(AudioClip sound)
    {
        AudioManager.Instance.PlaySFX(sound);
    }
}
