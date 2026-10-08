using UnityEngine;

public class TutorialTrigger : TriggerArea
{
    public TutorialUIManager uiManager;
    public override void OnEnter()
    {
        uiManager.TutorialOne();
    }

    public override void OnExit() { }

    public override void OnStay() { }
}
