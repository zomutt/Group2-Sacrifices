using UnityEngine;

public abstract class TriggerArea : MonoBehaviour
{
    public abstract void OnEnter();
    
    public abstract void OnExit();
    
    public abstract void OnStay();
}
