using UnityEngine;

public abstract class PayablePopup : PopupTrigger
{
    [SerializeField]
    private int soulCost;
    public int SoulCost
    {
        get => soulCost;
    }
    
    public virtual bool IsAvailable
        => true;
    public virtual string UnavailableText
        => "N/A";
    
    protected override void PassPopupToLevelManagement()
    {
        LevelManager.Instance.SetPaymentPanel(this);
    }
    
    public abstract void PaidAction();
}
