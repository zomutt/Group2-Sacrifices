
using UnityEngine;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject tutorialPanel;
    public GameObject tutorialText;
    public GameObject tutorialText2;
    public GameObject tutorialText3;
    public GameObject tutorialText4;
    public GameObject tutorialText5;
    public GameObject tutorialStartText;


    public GameObject yesButton;
    public GameObject noButton;
    public GameObject nextButton;
    public GameObject closeButton;

    [Header("Tutorial Settings")]
    public bool tutorialOn = false;

    [Header("Tutorial 2 Pages")]
    public GameObject tutorial2Page1;
    public GameObject tutorial2Page2;

    private int tutorial2Page = 1;

    private TMP_Text nextButtonText;

    private void Start()
    {
        nextButtonText =
            nextButton.GetComponentInChildren<TMP_Text>();

        HideAllTutorialTexts();

        tutorialPanel.SetActive(true);
        tutorialStartText.SetActive(true);
        yesButton.SetActive(true);
        noButton.SetActive(true);
        nextButton.SetActive(false);
        closeButton.SetActive(false);

        Time.timeScale = 0f;
    }

    private void HideAllTutorialTexts()
    {
        tutorialStartText.SetActive(false);
        tutorialText.SetActive(false);
        tutorialText2.SetActive(false);
        tutorialText3.SetActive(false);
        tutorialText4.SetActive(false);
        tutorialText5.SetActive(false);

        tutorial2Page1.SetActive(false);
        tutorial2Page2.SetActive(false);
    }

    private void OpenTutorial(GameObject textObject)
    {
        if (!tutorialOn) return;

        HideAllTutorialTexts();

        yesButton.SetActive(false);
        noButton.SetActive(false);
        nextButton.SetActive(false);
        closeButton.SetActive(true);

        tutorialPanel.SetActive(true);
        textObject.SetActive(true);

        Time.timeScale = 0f;
    }

    private void CloseTutorial(GameObject textObject)
    {
        textObject.SetActive(false);
        tutorialPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnClickYes()
    {
        tutorialOn = true;

        tutorialPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnClickNo()
    {
        tutorialOn = false;

        tutorialPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        Time.timeScale = 1f;
    }

    public void TutorialOne()
    {
        OpenTutorial(tutorialText);
    }

    public void TutorialTwo()
    {
        if (!tutorialOn) return;

        HideAllTutorialTexts();

        tutorial2Page = 1;

        tutorialPanel.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        tutorial2Page1.SetActive(true);
        nextButton.SetActive(true);

        if (nextButtonText != null)
            nextButtonText.text = "Next";

        Time.timeScale = 0f;
    }

    public void NextTutorialTwoPage()
    {
        if (tutorial2Page == 1)
        {
            tutorial2Page1.SetActive(false);
            tutorial2Page2.SetActive(true);

            tutorial2Page = 2;

            if (nextButtonText != null)
                nextButtonText.text = "Done";
        }
        else
        {
            CloseTutorialTwo();
        }
    }

    public void CloseTutorialTwo()
    {
        tutorial2Page1.SetActive(false);
        tutorial2Page2.SetActive(false);

        nextButton.SetActive(false);
        tutorialPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void TutorialThree()
    {
        OpenTutorial(tutorialText3);
    }

    public void TutorialFour()
    {
        OpenTutorial(tutorialText4);
    }

    public void TutorialFive()
    {
        OpenTutorial(tutorialText5);
    }

    public void CloseTutorialOne()
    {
        CloseTutorial(tutorialText);
    }

    public void CloseTutorialThree()
    {
        CloseTutorial(tutorialText3);
    }

    public void CloseTutorialFour()
    {
        CloseTutorial(tutorialText4);
    }

    public void CloseTutorialFive()
    {
        CloseTutorial(tutorialText5);
    }
}