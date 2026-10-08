using UnityEngine;

public class StatusEffect : ScriptableObject
{
    public bool permanent;
    public bool hidden;
    
    [SerializeField]
    private float duration;
    public float Duration => duration;
    
    public int power;
    
    public Sprite icon;
    
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
