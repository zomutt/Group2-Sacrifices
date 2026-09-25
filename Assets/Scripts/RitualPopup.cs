using UnityEngine;

public class RitualPopup : PopupTrigger
{
    [SerializeField]
    private int soulReward;
    public int SoulReward
    {
        get => soulReward;
    }
    
    protected override void PassPopupToLevelManagement()
    {
        LevelManager.Instance.SetSacrificePanel(this);
    }
    
    public void RewardAction()
    {
        Debug.Log("Haha! You're welcome!");
    }
}
