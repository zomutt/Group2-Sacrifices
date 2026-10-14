using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/MassAttack")]
public class MassAttack : AttackBehavior
{
    [SerializeField]
    private AttackBehavior attackToUse;
    [SerializeField] private AudioClip[] sounds;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        OnHitSounds();
        
        List<CharacterBehavior> targets = new List<CharacterBehavior>
            (to.isEnemy ? CombatManager.Instance.enemyCharacters : CombatManager.Instance.partyCharacters);
        
        foreach (CharacterBehavior chara in targets)
        {
            attackToUse.MoveOnTarget(from, chara);
        }
    }

    private void OnHitSounds()
    {
        if (!AudioManager.Instance) return;

        foreach (AudioClip sound in sounds)
            AudioManager.Instance.PlaySFX(sound);
    }
}
