using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public bool permanent;
    [SerializeField]
    private bool hidden;
    
    [SerializeField]
    private float duration;
    
    public int power;
    
    public virtual void TickStatusEffect(CharacterBehavior owner)
    {
        if (!permanent)
        {
            duration -= Time.deltaTime;
            
            if (duration <= 0)
                owner.statusesToRemove.Add(this);
        }
    }
    
    public virtual void DoTurnStart(CharacterBehavior owner) {}
    
    public virtual void DoOnHurtMelee(CharacterBehavior owner, CharacterBehavior attacker) {}
    
    public virtual void DoOnAdd(CharacterBehavior owner) {}
    
    public virtual void DoOnEnd(CharacterBehavior owner) {}
}
