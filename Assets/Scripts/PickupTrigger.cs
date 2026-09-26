using UnityEngine;

public class PickupTrigger : TriggerArea
{
    [SerializeField]
    private int soulValue;
    
    #region Trigger
    public override void OnEnter()
    {
        LevelManager.Instance.GainSouls(soulValue);
        
        gameObject.SetActive(false);
    }

    public override void OnExit() {}

    public override void OnStay() {}
    #endregion
}
