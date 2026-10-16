using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Activate/Deactivate")]
    [Tooltip("Anything in activate group is set active in start, same logic for deactivate group.")]
    [SerializeField] private GameObject[] activateGroup;
    [SerializeField] private GameObject[] deactivateGroup;
    
    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject difficultySelect;

    [Header("Help Panel")]
    [Tooltip("Help pages that get flipped through. Add more here if needed.")]
    [SerializeField] private GameObject[] helpSubPanels;
    private int currentHelpPage = 0;

    [Header("Scenes")]
    [SerializeField] private string creditsScene = "CreditsScene";
    [SerializeField] private string levelOne = "Level_1";

    [Header("Audio")]
    [SerializeField] private AudioClip clickSound;



    private void Start()
    {
        ActivateAllInGroup();
        DeactivateAllInGroup();
    }

    private void ActivateAllInGroup()
    {
        foreach (var panel in activateGroup)
        {
            if (!panel) continue;
            panel.SetActive(true);
        }
    }

    private void DeactivateAllInGroup()
    {
        foreach (var panel in deactivateGroup)
        {
            if (!panel) continue;
            panel.SetActive(false);
        }
    }

    public void OnClickPlay()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        
        difficultySelect.SetActive(true);
    }

    public void OnClickToggleSettings()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        if (!settingsPanel) return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        if (helpPanel) helpPanel.SetActive(false);   // Close help so they don't overlap
    }

    public void OnClickToggleHelp()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        if (!helpPanel) return;
        ResetHelpPages();   // Back to the first page when you open/close the panel
        helpPanel.SetActive(!helpPanel.activeSelf);

        if (settingsPanel) settingsPanel.SetActive(false);   // Close settings so they don't overlap
    }

    public void OnClickCredits()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        Debug.Log("Loading credits");
        SceneManager.LoadScene(creditsScene);
    }

    public void OnClickExitGame()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void OnClickCycleHelp()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        if (helpSubPanels.Length == 0) return;
        currentHelpPage++;
        if (currentHelpPage >= helpSubPanels.Length) currentHelpPage = 0;
        CloseAllHelpPanels();
        if (helpSubPanels[currentHelpPage]) helpSubPanels[currentHelpPage].SetActive(true);
    }

    private void ResetHelpPages()
    {
        if (helpSubPanels.Length == 0) return;
        currentHelpPage = 0;
        CloseAllHelpPanels();
        if (helpSubPanels[currentHelpPage]) helpSubPanels[currentHelpPage].SetActive(true);
    }

    private void CloseAllHelpPanels()
    {
        foreach (var panel in helpSubPanels)
        {
            if (!panel) continue;
            panel.SetActive(false);
        }
    }
    
    public void SetDifficultyValueAndStart(int value)
    {
        StoredSettings.difficulty = value;
        StoredSettings.score = 0;
        
        Debug.Log("Starting level one");
        SceneManager.LoadScene(levelOne);
    }
}
