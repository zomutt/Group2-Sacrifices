using UnityEngine;

public class EncounterTrigger : TriggerArea
{
    #region Trigger
    public override void OnEnter() {}

    public override void OnExit() {}

    public override void OnStay()
    {
        LevelManager.Instance.ProgressEncounterTimer(Time.fixedDeltaTime);
    }
    #endregion
}
