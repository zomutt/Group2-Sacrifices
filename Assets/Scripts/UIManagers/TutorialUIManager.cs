
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject tutorialPanel;
    public GameObject tutorialText;
    public GameObject tutorialText2;
    public GameObject yesButton, noButton;

    [Header("Tutorial Settings")]
    public bool tutorialOn = false;

    void Start()
    {
        // Pause the game while asking the player about tutorials.
        Time.timeScale = 0f;

        // Show the initial Yes/No prompt.
        tutorialPanel.SetActive(true);

        // Hide individual tutorial panels.
        tutorialText.SetActive(false);
        tutorialText2.SetActive(false);
    }

    public void OnClickYes()
    {
        // Enable tutorials for this play session.
        tutorialOn = true;

        // Close the initial prompt.
        yesButton.SetActive(false);
        noButton.SetActive(false);
        tutorialPanel.SetActive(false);

        // Resume gameplay.
        Time.timeScale = 1f;
    }

    public void OnClickNo()
    {
        // Disable tutorials for this play session.
        tutorialOn = false;

        // Close the initial prompt.
        tutorialPanel.SetActive(false);

        // Resume gameplay.
        Time.timeScale = 1f;
    }

    public void TutorialOne()
    {
        if (!tutorialOn) return;

        // Pause gameplay and display the first tutorial.
        tutorialPanel.SetActive(true);
        tutorialText.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TutorialTwo()
    {
        if (!tutorialOn) return;

        // Pause gameplay and display the second tutorial.
        tutorialPanel.SetActive(true);
        tutorialText2.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void CloseTutorialOne()
    {
        tutorialPanel.SetActive(false);
        tutorialText.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CloseTutorialTwo()
    {
        tutorialText2.SetActive(false);
        Time.timeScale = 1f;
    }
}