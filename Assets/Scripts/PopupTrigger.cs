using UnityEngine;

public abstract class PopupTrigger : TriggerArea
{
    #region Trigger
    public override void OnEnter()
    {
        PassPopupToLevelManagement();
    }

    public override void OnExit()
    {
        if (LevelManager.Instance.currentPopup == this)
            LevelManager.Instance.ClearPopupPanel();
    }

    public override void OnStay() {}
    #endregion
    
    protected abstract void PassPopupToLevelManagement();
}
