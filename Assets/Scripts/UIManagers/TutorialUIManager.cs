using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    public GameObject tutorialPanel, tutorialText, tutorialText2, soulTutorialTrigger;
    public bool tutorialOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        tutorialPanel.SetActive(true);
        tutorialText.SetActive(false);
        tutorialText2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickYes()
    {
        tutorialText.SetActive(true);
        tutorialOn = true;
        tutorialPanel.SetActive(false);
        tutorialText.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickNo()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void TutorialOne()
    {
        Time.timeScale = 0;
        tutorialPanel.SetActive(true);
        tutorialText2.SetActive(true);
        Destroy(soulTutorialTrigger);
    }

}
