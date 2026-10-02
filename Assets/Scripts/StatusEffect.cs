using UnityEngine;

public class StatusEffect : ScriptableObject
{
    [SerializeField]
    private bool permanent;
    [SerializeField]
    private bool hidden;
    
    private float duration;
    
    public virtual void TickStatusEffect()
    {
        duration -= Time.deltaTime;
    }
    
    public virtual void DoTurnStart() {}
}
