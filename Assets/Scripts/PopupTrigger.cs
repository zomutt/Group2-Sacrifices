using UnityEngine;

public class PopupTrigger : TriggerArea
{
    #region Trigger
    public override void OnEnter()
    {
        LevelManager.Instance.SetTriggeredPanel(this);
    }

    public override void OnExit()
    {
        if (LevelManager.Instance.currentPopup == this)
            LevelManager.Instance.SetTriggeredPanel(null);
    }

    public override void OnStay() {}
    #endregion
}
