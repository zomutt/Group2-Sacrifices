
using UnityEngine;

public class TutorialTrigger : TriggerArea
{
    public TutorialUIManager uiManager;

    [Header("Tutorial Settings")]
    public int tutorialNumber = 1;

    public override void OnEnter()
    {
        if (uiManager == null || !uiManager.tutorialOn)
            return;

        switch (tutorialNumber)
        {
            case 1:
                uiManager.TutorialOne();
                break;

            case 2:
                uiManager.TutorialTwo();
                break;
        }

        // Prevent this tutorial trigger from activating again.
        gameObject.SetActive(false);
    }

    public override void OnExit() { }

    public override void OnStay() { }
}