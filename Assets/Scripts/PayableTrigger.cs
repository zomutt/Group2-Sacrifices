using UnityEngine;

public abstract class PayableTrigger : TriggerArea
{
    [SerializeField]
    private int soulCost;
    public int SoulCost
    {
        get => soulCost;
    }
    
    #region Trigger
    public override void OnEnter()
    {
        LevelManager.Instance.currentPayable = this;
    }

    public override void OnExit()
    {
        if (LevelManager.Instance.currentPayable == this)
            LevelManager.Instance.currentPayable = null;
    }

    public override void OnStay() {}
    #endregion
    
    public abstract void PaidAction();
}
