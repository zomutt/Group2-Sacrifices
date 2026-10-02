using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/AddStatusMove")]
public class AddStatusMove : AttackBehavior
{
    public StatusEffect statusToAdd;
    
    public bool usesMagic;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        StatusEffect newStatusEffect = Instantiate(statusToAdd);
        
        if (usesMagic)
            newStatusEffect.power = (int)(GetMagicPowerOf(from) * power);
        
        to.statuses.Add(newStatusEffect);
    }
}
