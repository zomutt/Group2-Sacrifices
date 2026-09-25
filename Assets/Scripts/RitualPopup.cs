using UnityEngine;

public class RitualPopup : PopupTrigger
{
    protected override void PassPopupToLevelManagement()
    {
        LevelManager.Instance.SetSacrificePanel(this);
    }
}
