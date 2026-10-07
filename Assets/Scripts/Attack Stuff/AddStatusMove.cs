using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/AddStatus")]
public class AddStatusMove : PlainAttack
{
    public List<StatusEffect> statusesToAdd;
    
    public bool usesMagic;
    public float magicMultiplier;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        base.MoveOnTarget(from, to);
        
        foreach (StatusEffect status in statusesToAdd)
        {
            StatusEffect newStatusEffect = Instantiate(status);
            
            if (usesMagic)
                newStatusEffect.power = (int)(GetMagicPowerOf(from) * magicMultiplier);
            
            to.AddStatusEffect(newStatusEffect);
        }
    }
}
