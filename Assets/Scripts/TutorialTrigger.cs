
using UnityEngine;
using System.Collections;

public class TutorialTrigger : TriggerArea
{
    public TutorialUIManager uiManager;

    [Header("Tutorial Settings")]
    [Range(1, 5)]
    public int tutorialNumber = 1;

    public override void OnEnter()
    {
        if (uiManager == null || !uiManager.tutorialOn)
            return;

        // Tutorial 2 needs a short delay before appearing.
        if (tutorialNumber == 2)
        {
            StartCoroutine(DelayedTutorialTwo());
            return;
        }

        ShowSelectedTutorial();

        // Prevent this trigger from activating again.
        gameObject.SetActive(false);
    }

    private void ShowSelectedTutorial()
    {
        switch (tutorialNumber)
        {
            case 1:
                uiManager.TutorialOne();
                break;

            case 2:
                uiManager.TutorialTwo();
                break;

            case 3:
                uiManager.TutorialThree();
                break;

            case 4:
                uiManager.TutorialFour();
                break;

            case 5:
                uiManager.TutorialFive();
                break;

            default:
                Debug.LogWarning(
                    "Invalid tutorial number: " + tutorialNumber,
                    this
                );
                break;
        }
    }

    private IEnumerator DelayedTutorialTwo()
    {
        // WaitForSecondsRealtime works even when timeScale is zero.
        yield return new WaitForSecondsRealtime(2.0f);

        if (uiManager != null && uiManager.tutorialOn)
            uiManager.TutorialTwo();

        // Prevent this trigger from activating again.
        gameObject.SetActive(false);
    }

    public override void OnExit() { }

    public override void OnStay() { }
}