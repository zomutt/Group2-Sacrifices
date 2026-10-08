using UnityEngine;

public class TutorialTrigger : TriggerArea
{
    public TutorialUIManager uiManager;
    public override void OnEnter()
    {
        if(uiManager.tutorialOn)
        {
            uiManager.TutorialOne();
        }
    }

    public override void OnExit() { }

    public override void OnStay() { }
}
