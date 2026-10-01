using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Activate/Deactivate All")] 
    [Tooltip("Place anything here you want active on start")]
    [SerializeField] private GameObject[] activateGroup;
    [Tooltip("Place anything here you want inactive on start")]
    [SerializeField] private GameObject[] deactivateGroup;

    [Header("Pause Menu Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject confirmQuitPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Help Panels")]
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject[] helpSubPanels;
    private int currentHelpPage = 0;

    [Header("Souls")]
    [SerializeField] private GameObject soulsPanel;
    [SerializeField] private TextMeshProUGUI soulsText;

    [Header("Overworld UI")]
    [SerializeField] private GameObject overworldPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PaymentPanel paymentPanel;
    [SerializeField] private RitualPanel ritualPanel;

    // LevelManager grabs these instead of searching by name
    public PaymentPanel PaymentPanel => paymentPanel;
    public RitualPanel RitualPanel => ritualPanel;

    [Header("Scenes")]
    [SerializeField] private string creditsScene = "CreditsScene";
    [SerializeField] private string mainMenuScene = "MainMenuScene";
    [SerializeField] private string levelOneScene = "Level_1";

    [Header("Combat UI")]
    [SerializeField] private GameObject battleStatsPanel;

    [Header("Player Stats")]
    [FormerlySerializedAs("fadedUI")] [SerializeField] private CombatantUI fatedUI;

    [Header("Enemy Stats")]
    [SerializeField] private CombatantUI enemyUI;

    [Header("Party")]
    [Tooltip("Fated1-4 in order")]
    [SerializeField] private PartyMemberUI[] partyUI;

    public static UIController Instance;

    [System.Serializable]
    private class CombatantUI      // One per side, player (P_) and enemy (E_)
    {
        public TextMeshProUGUI nameText;
        public Image icon;
        public TextMeshProUGUI dmgText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI armorText;
        public TextMeshProUGUI dodgeText;
        public TextMeshProUGUI critText;
        public TextMeshProUGUI[] abilityTexts;
        public Image[] abilityImages;
    }

    [System.Serializable]
    private class PartyMemberUI
    {
        public TextMeshProUGUI nameText;
        public Image portrait;
        public Slider healthBar;
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);   // One already carried over from another scene
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;   // So it doesn't point at a dead UI after a scene change
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;

        bool onMainMenu = scene.name == mainMenuScene;
        gameObject.SetActive(!onMainMenu);
        if (onMainMenu) return;

        ActivateAllInGroup();
        DeactivateAllInGroup();
    }

    private void Start()
    {
        ActivateAllInGroup();
        DeactivateAllInGroup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel) // Not gonna touch your input system, so I'll use this for now
        {
            if (pausePanel.activeSelf) OnClickResumeGame();
            else OnClickPauseGame();
        }
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
    
    #region Pause Menu
    public void OnClickReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickCloseGame()
    {
        Application.Quit();
    }
    public void OnClickPauseGame()
    {
        Time.timeScale = 0; // This lives here (in UIController) because it's super early on, not because it's best practice.
        if (pausePanel) pausePanel.SetActive(true);
    }

    public void OnClickResumeGame()
    {
        Time.timeScale = 1; // Same as above.
        if (pausePanel) pausePanel.SetActive(false);

        // Close everything so it doesn't pop back up next pause
        if (settingsPanel) settingsPanel.SetActive(false);
        if (helpPanel) helpPanel.SetActive(false);
        if (confirmQuitPanel) confirmQuitPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        Time.timeScale = 0;
        if (losePanel) losePanel.SetActive(true);
    }

    public void OnClickBeginQuit()
    {
        if (confirmQuitPanel) confirmQuitPanel.SetActive(true);
    }

    public void OnClickDenyQuit()
    {
        if (confirmQuitPanel) confirmQuitPanel.SetActive(false);
    }

    public void OnClickConfirmQuit()
    {
        Time.timeScale = 1; // Otherwise the main menu loads frozen
        Destroy(gameObject);   // Don't drag the game UI into the menu
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnClickToggleSettings()
    {
        if (!settingsPanel) return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        if (helpPanel) helpPanel.SetActive(false);
    }

    public void OnClickToggleHelp()
    {
        if (!helpPanel) return;
        ResetHelpPages(); // Back to the first page when you open/close the panel
        helpPanel.SetActive(!helpPanel.activeSelf);

        if (settingsPanel) settingsPanel.SetActive(false);
    }

    public void OnClickCycleHelp()
    {
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
    #endregion

    #region Combat
    // ===== Combat =====
    // Takes plain values so it doesn't care how stats are stored. Change it if it doesn't fit.

    public void StartBattleUI()
    {
        if (battleStatsPanel) battleStatsPanel.SetActive(true);
    }

    public void EndBattleUI()
    {
        if (battleStatsPanel) battleStatsPanel.SetActive(false);
    }

    // Fated/Enemy versions just point the same code at the right side.
    public void SetFatedInfo(string charName, Sprite portrait) => SetInfo(fatedUI, charName, portrait);
    public void SetEnemyInfo(string charName, Sprite portrait) => SetInfo(enemyUI, charName, portrait);

    // Dodge/crit are whole numbers, 15 = 15%
    public void SetFatedStats(int damage, int health, int maxHealth, int armor, int dodge, int crit) => SetStats(fatedUI, damage, health, maxHealth, armor, dodge, crit);
    public void SetEnemyStats(int damage, int health, int maxHealth, int armor, int dodge, int crit) => SetStats(enemyUI, damage, health, maxHealth, armor, dodge, crit);

    // Slot is 0-3, anything else is ignored. The [1]: gets added automatically
    public void SetFatedAbility(int slot, string abilityName, Sprite icon) => SetAbility(fatedUI, slot, abilityName, icon);
    public void SetEnemyAbility(int slot, string abilityName, Sprite icon) => SetAbility(enemyUI, slot, abilityName, icon);

    private void SetInfo(CombatantUI ui, string charName, Sprite portrait)
    {
        if (ui.nameText) ui.nameText.text = charName;
        if (ui.icon) ui.icon.sprite = portrait;
    }

    private void SetStats(CombatantUI ui, int damage, int health, int maxHealth, int armor, int dodge, int crit)
    {
        if (ui.dmgText) ui.dmgText.text = $"Damage: {damage}";
        if (ui.healthText) ui.healthText.text = $"Health: {health}/{maxHealth}";
        if (ui.armorText) ui.armorText.text = $"Armor: {armor}";
        if (ui.dodgeText) ui.dodgeText.text = $"Dodge Chance: {dodge}%";
        if (ui.critText) ui.critText.text = $"Crit Chance: {crit}%";
    }

    private void SetAbility(CombatantUI ui, int slot, string abilityName, Sprite icon)
    {
        if (slot < 0) return;
        if (slot < ui.abilityTexts.Length && ui.abilityTexts[slot]) ui.abilityTexts[slot].text = $"[{slot + 1}]: {abilityName}";
        if (slot < ui.abilityImages.Length && ui.abilityImages[slot]) ui.abilityImages[slot].sprite = icon;
    }

    // Goes wherever GainSouls/ConfirmSoulPayment are
    public void SetSouls(int souls)
    {
        if (soulsText) soulsText.text = $"Souls: {souls}";
    }
    #endregion
    
    #region Overworld
    // ===== Overworld =====

    public void SetOverworldUI(bool active)
    {
        if (overworldPanel) overworldPanel.SetActive(active);
    }

    public void SetTimer(int time)
    {
        if (timerText) timerText.text = time.ToString();
    }

    // ===== Party =====
    // Call when adding/replacing a slot.
    // Slot is 0-3, same order as Fated1-4.

    public void SetPartyMember(int slot, string charName, Sprite portrait)
    {
        if (slot < 0 || slot >= partyUI.Length) return;
        if (partyUI[slot].nameText) partyUI[slot].nameText.text = charName;
        if (partyUI[slot].portrait) partyUI[slot].portrait.sprite = portrait;
    }

    public void SetPartyHealth(int slot, int health, int maxHealth)
    {
        if (slot < 0 || slot >= partyUI.Length || !partyUI[slot].healthBar) return;
        partyUI[slot].healthBar.maxValue = maxHealth;
        partyUI[slot].healthBar.value = health;
    }
    #endregion
}
