using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Activate/Deactivate")]
    [Tooltip("Anything in activate group is set active in start, same logic for deactivate group.")]
    [SerializeField] private GameObject[] activateGroup;    // Put anything here you want active in start.
    [SerializeField] private GameObject[] deactivateGroup;  // See above, but inactive.

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject helpPanel;

    [Header("Help Panel")]
    [Tooltip("Help pages that get flipped through. Add more here if needed.")]
    [SerializeField] private GameObject[] helpSubPanels;
    private int currentHelpPage = 0;

    [Header("Scenes")]
    [SerializeField] private string creditsScene = "CreditsScene";

    private void Start()
    {
        ActivateAllInGroup();
        DeactivateAllInGroup();
    }

    private void ActivateAllInGroup()
    {
        foreach (var panel in activateGroup)
        {
            panel.SetActive(true);
        }
    }

    private void DeactivateAllInGroup()
    {
        foreach (var panel in deactivateGroup)
        {
            panel.SetActive(false);
        }
    }

    public void OnClickPlay()
    {
        Debug.Log("Next scene (put scene here when we have it)");
        // Logic here once we have an established scene.
    }

    public void OnClickToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        helpPanel.SetActive(false);   // Close help so they don't overlap
    }

    public void OnClickToggleHelp()
    {
        ResetHelpPages();   // Back to the first page when you open/close the panel
        helpPanel.SetActive(!helpPanel.activeSelf);

        settingsPanel.SetActive(false);   // Close settings so they don't overlap
    }

    public void OnClickCredits()
    {
        Debug.Log("Loading credits");
        SceneManager.LoadScene(creditsScene);
    }

    public void OnClickExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void OnClickCycleHelp()
    {
        currentHelpPage++;
        if (currentHelpPage >= helpSubPanels.Length) currentHelpPage = 0;
        CloseAllHelpPanels();
        helpSubPanels[currentHelpPage].SetActive(true);
    }

    private void ResetHelpPages()
    {
        currentHelpPage = 0;
        CloseAllHelpPanels();
        helpSubPanels[currentHelpPage].SetActive(true);
    }

    private void CloseAllHelpPanels()
    {
        foreach (var panel in helpSubPanels)
        {
            panel.SetActive(false);
        }
    }
}
