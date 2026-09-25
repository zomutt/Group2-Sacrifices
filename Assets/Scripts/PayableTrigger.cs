using UnityEngine;

public abstract class PayablePopup : PopupTrigger
{
    [SerializeField]
    private int soulCost;
    public int SoulCost
    {
        get => soulCost;
    }
    
    public abstract void PaidAction();
}
