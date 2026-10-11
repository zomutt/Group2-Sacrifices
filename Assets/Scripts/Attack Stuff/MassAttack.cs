using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/MassAttack")]
public class MassAttack : AttackBehavior
{
    [SerializeField]
    private AttackBehavior attackToUse;
    [SerializeField] private AudioClip[] sounds;

    protected override void OnHitSpecialFX(CharacterBehavior target)
    {
        if (!AudioManager.Instance) return;

        foreach (AudioClip sound in sounds)
            AudioManager.Instance.PlaySFX(sound);
    }
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        OnHitSpecialFX(to);

        List<CharacterBehavior> targets = (to.isEnemy) ?
            CombatManager.Instance.enemyCharacters : CombatManager.Instance.partyCharacters;
        
        foreach (CharacterBehavior chara in targets)
        {
            attackToUse.MoveOnTarget(from, chara);
        }
    }
}
