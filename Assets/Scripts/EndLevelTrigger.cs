using UnityEngine;

public class EndLevelTrigger : TriggerArea
{
    #region Trigger
    public override void OnEnter()
    {
        LevelManager.Instance.EndLevel();
        
        gameObject.SetActive(false);
    }

    public override void OnExit() {}

    public override void OnStay() {}
    #endregion
}
