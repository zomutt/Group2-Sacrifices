using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/MassAttack")]
public class MassAttack : AttackBehavior
{
    [SerializeField]
    private AttackBehavior attackToUse;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        List<CharacterBehavior> targets = (to.isEnemy) ?
            CombatManager.Instance.enemyCharacters : CombatManager.Instance.partyCharacters;
        
        foreach (CharacterBehavior chara in targets)
        {
            attackToUse.MoveOnTarget(from, chara);
        }
    }
}
