using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/Summon")]
public class SummonMove : AttackBehavior
{
    [SerializeField]
    private GameObject summonedCharacter;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        CombatManager.Instance.AddToBattle(summonedCharacter);
    }
}
