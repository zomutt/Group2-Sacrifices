using UnityEngine;

public class StatusEffect : ScriptableObject
{
    [SerializeField]
    private bool permanent;
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
}
