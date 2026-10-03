using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/AddStatusMove")]
public class AddStatusMove : PlainAttack
{
    public StatusEffect statusToAdd;
    
    public bool usesMagic;
    public float magicMultiplier;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        base.MoveOnTarget(from, to);
        
        StatusEffect newStatusEffect = Instantiate(statusToAdd);
        
        if (usesMagic)
            newStatusEffect.power = (int)(GetMagicPowerOf(from) * magicMultiplier);
        
        to.statuses.Add(newStatusEffect);
    }
}
