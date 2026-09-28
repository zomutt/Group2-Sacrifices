using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Activate/Deactivate All")] 
    [Tooltip("Place anything here you want active on start")] 
    [SerializeField] private GameObject[] activateGroup;
    [SerializeField] private GameObject[] deactivateGroup;

    [Header("Pause Menu Panels")] 
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject confirmQuitPanel;
    
    [Header("Help Panels")]
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject[] helpSubPanels;
    private int currentHelpPage = 0;

    [Header("Scenes")] 
    [SerializeField] private string creditsScene = "CreditsScene";
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    
    [Header("Combat UI")]
    [SerializeField] private GameObject combatUIPlayer;
    [SerializeField] private GameObject combatUIEnemy;
    // Add level 1, 2, 3

    private void Start()
    {
         ActivateAllInGroup();
         DeactivateAllInGroup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Not gonna touch your input system, so I'll use this for now
        {
            OnClickPauseGame();
        }
    }
    private void ActivateAllInGroup()
    {
        foreach (var panel in activateGroup)
        {
            if (!panel) return;
            panel.SetActive(true);
        }
    }

    private void DeactivateAllInGroup()
    {
        foreach (var panel in deactivateGroup)
        {
            if (!panel) return;
            panel.SetActive(false);
        }
    }

    /// <summary>
    /// PAUSE MENU
    /// </summary>
    
    public void OnClickPauseGame()
    {
        Time.timeScale = 0;    // I know this should go in GameManager, but it lives here for testing.
        pausePanel.SetActive(true);
    }
    
    public void OnClickResumeGame()
    {
        Time.timeScale = 1;      // Same as above.
        pausePanel.SetActive(false);
    }

    public void OnClickBeginQuit()    // They have to confirm before they quit
    {
        confirmQuitPanel.SetActive(true);
    }

    public void OnClickDenyQuit()
    {
        confirmQuitPanel.SetActive(false);
    }
    
    public void OnClickConfirmQuit()
    {
        SceneManager.LoadScene(mainMenuScene);
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
    
    public void OnClickCycleHelp()
    {
        currentHelpPage++;
        if (currentHelpPage >= helpSubPanels.Length) currentHelpPage = 0;
        CloseAllHelpPanels();
        helpSubPanels[currentHelpPage].SetActive(true);
    }
    
    private void ResetHelpPages()   // Called so that the player starts at the beginning of help on each load rather than the middle.
    {
        currentHelpPage = 0;
        CloseAllHelpPanels();
        helpSubPanels[currentHelpPage].SetActive(true);
    }
    
    private void CloseAllHelpPanels()
    {
        foreach (var panel in helpSubPanels)
        {
            if (!panel) return;
            panel.SetActive(false);
        }
    }
    
    //////////////////
    ///
    //////////////////
    private void UpdateCombatUI()
    {
        
    }

    private void UpdateSouls()
    {
        
    }

    public void StartBattleUI()
    {
        
    }
}
