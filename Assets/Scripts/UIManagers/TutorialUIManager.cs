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
    public GameObject[] tutorial2Pages;
    public GameObject[] tutorial2PagesB;

    private int currentTutorial2Page = 0;
    private TMP_Text nextButtonText;
    
    public static TutorialUIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        if (nextButton != null)
            nextButtonText = nextButton.GetComponentInChildren<TMP_Text>();

        HideAllTutorialTexts();

        tutorialPanel.SetActive(false);
        tutorialStartText.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        nextButton.SetActive(false);
        closeButton.SetActive(false);

        //Time.timeScale = 0f;
        
        tutorialOn =
            StoredSettings.difficulty == 1 && StoredSettings.playCount <= 1;
        Debug.Log(tutorialOn);
    }

    private void HideAllTutorialTexts()
    {
        tutorialStartText.SetActive(false);
        tutorialText.SetActive(false);
        tutorialText2.SetActive(false);
        tutorialText3.SetActive(false);
        tutorialText4.SetActive(false);
        tutorialText5.SetActive(false);

        if (tutorial2Pages != null)
        {
            foreach (GameObject page in tutorial2Pages)
            {
                if (page != null)
                    page.SetActive(false);
            }
        }
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

        closeButton.SetActive(false);
        nextButton.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnClickYes()
    {
        tutorialOn = true;

        tutorialPanel.SetActive(false);
        tutorialStartText.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnClickNo()
    {
        tutorialOn = false;

        tutorialPanel.SetActive(false);
        tutorialStartText.SetActive(false);
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

        if (tutorial2Pages == null || tutorial2Pages.Length == 0)
        {
            Debug.LogWarning("Tutorial 2 has no pages assigned!");
            return;
        }

        HideAllTutorialTexts();

        tutorialPanel.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        closeButton.SetActive(false);
        nextButton.SetActive(true);

        currentTutorial2Page = 0;
        ShowCurrentTutorial2Page();

        Time.timeScale = 0f;
    }

    private void ShowCurrentTutorial2Page()
    {
        foreach (GameObject page in tutorial2Pages)
        {
            if (page != null)
                page.SetActive(false);
        }

        GameObject currentPage = tutorial2Pages[currentTutorial2Page];

        if (currentPage == null)
        {
            Debug.LogWarning("A Tutorial 2 page is missing!");
            return;
        }

        currentPage.SetActive(true);

        if (nextButtonText != null)
        {
            nextButtonText.text =
                currentTutorial2Page == tutorial2Pages.Length - 1
                ? "Done"
                : "Next";
        }
    }

    public void TutorialTwoB()
    {
        if (!tutorialOn) return;

        if (tutorial2PagesB == null || tutorial2PagesB.Length == 0)
        {
            Debug.LogWarning("Tutorial 2 has no pages assigned!");
            return;
        }

        isTutB = true;
        HideAllTutorialTexts();

        tutorialPanel.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        closeButton.SetActive(false);
        nextButton.SetActive(true);

        currentTutorial2Page = 0;
        ShowCurrentTutorial2PageB();

        Time.timeScale = 0f;
    }

    private void ShowCurrentTutorial2PageB()
    {
        foreach (GameObject page in tutorial2PagesB)
        {
            if (page != null)
                page.SetActive(false);
        }

        GameObject currentPage = tutorial2PagesB[currentTutorial2Page];

        if (currentPage == null)
        {
            Debug.LogWarning("A Tutorial 2 page is missing!");
            return;
        }

        currentPage.SetActive(true);

        if (nextButtonText != null)
        {
            nextButtonText.text =
                currentTutorial2Page == tutorial2PagesB.Length - 1
                ? "Done"
                : "Next";
        }
    }
    
    private bool isTutB;
    
    public void NextTutorialTwoPage()
    {
        if (isTutB)
        {
            if (tutorial2PagesB == null || tutorial2PagesB.Length == 0)
            return;

        if (currentTutorial2Page < tutorial2PagesB.Length - 1)
        {
            currentTutorial2Page++;
            ShowCurrentTutorial2PageB();
        }
        else
        {
            CloseTutorialTwo();
        }
            
            return;
        }
        
        if (tutorial2Pages == null || tutorial2Pages.Length == 0)
            return;

        if (currentTutorial2Page < tutorial2Pages.Length - 1)
        {
            currentTutorial2Page++;
            ShowCurrentTutorial2Page();
        }
        else
        {
            CloseTutorialTwo();
        }
    }

    public void CloseTutorialTwo()
    {
        if (tutorial2Pages != null)
        {
            foreach (GameObject page in tutorial2Pages)
            {
                if (page != null)
                    page.SetActive(false);
            }
        }

        nextButton.SetActive(false);
        closeButton.SetActive(false);
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