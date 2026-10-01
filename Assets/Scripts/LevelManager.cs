using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float levelTime;
    
    private int souls;
    public int Souls
    {
        get => souls;
    }
    
    [HideInInspector]
    public PopupTrigger currentPopup;
    private GameObject currentPopupPanel;
    
    private PaymentPanel paymentPanel;
    private RitualPanel ritualPanel;
    
    private int encounterIndex;
    private float encounterTimer;
    [SerializeField]
    private EncounterInfo[] encounters;
    
    [SerializeField]
    private bool skipEncounters;
    
    private ObjectPooler damageNumberPool;
    
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI soulText;
    private GameObject overworldUI;
    
    public static GameState LevelGameState;
    public static LevelManager Instance;
    
    void Awake()
    {
        //timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        //soulText = GameObject.Find("SoulCount").GetComponent<TextMeshProUGUI>();
        
        #region Get Overworld UI
        overworldUI = GameObject.Find("OverworldOnly");
        
        paymentPanel = GameObject.Find("PurchasePanel").GetComponent<PaymentPanel>();
        paymentPanel.gameObject.SetActive(false);
        
        ritualPanel = GameObject.Find("RitualPanel").GetComponent<RitualPanel>();
        ritualPanel.gameObject.SetActive(false);
        #endregion
        
        damageNumberPool = GameObject.Find("DmgNumPool").GetComponent<ObjectPooler>();
        
        LevelGameState = GameState.Exploring;
        Instance = this;
    }

    void Start()
    {
        #region Get Overworld UI
        // In Start so UIController.Instance is guaranteed to be set
        paymentPanel = UIController.Instance.PaymentPanel;
        paymentPanel.gameObject.SetActive(false);
        
        ritualPanel = UIController.Instance.RitualPanel;
        ritualPanel.gameObject.SetActive(false);
        #endregion

        levelTime = 180f;
        
        souls = 0;
        
        encounterIndex = 0;
        encounterTimer = encounters[encounterIndex].spawnTime;
        
        CameraController.Instance.JumpToOverworldTarget();
    }
    
    void Update()
    {
        TickLevelTimer();
        
        UpdateUI();
    }
    
    private void TickLevelTimer()
    {
        if (levelTime > 0)
            levelTime -= Time.deltaTime;
        else
        {
            // Lose level
        }
    }
    
    private void UpdateUI()
    {
        UIController.Instance.SetTimer(Mathf.CeilToInt(levelTime / 2f));

        UIController.Instance.SetSouls(souls);
    }
    
    #region Souls
    public void GainSouls(int addedSouls)
    {
        souls += addedSouls;
    }
    
    public bool ConfirmSoulPayment(int amount)
    {
        if (souls >= amount)
        {
            souls -= amount;
            return true;
        }
        else
            return false;
    }
    #endregion
    
    #region Panels
    public void SetPaymentPanel(PayablePopup popup)
    {
        currentPopup = popup;
        
        currentPopupPanel = paymentPanel.gameObject;
        
        paymentPanel.gameObject.SetActive(true);
        paymentPanel.SetCost(popup);
    }
    
    public void SetSacrificePanel(RitualPopup popup)
    {
        currentPopup = popup;
        
        currentPopupPanel = ritualPanel.gameObject;
        
        ritualPanel.gameObject.SetActive(true);
        ritualPanel.SetReward(popup);
    }
    
    public void ClearPopupPanel()
    {
        currentPopup = null;
        currentPopupPanel.SetActive(false);
    }
    #endregion
    
    #region Encounters
    public void ProgressEncounterTimer(float time)
    {
        if (skipEncounters)
            return;
        
        encounterTimer -= time;
        
        if (encounterTimer <= 0)
        {
            TriggerEncounter(encounters[encounterIndex]);
            
            encounterIndex = (encounterIndex + 1) % encounters.Length;
            encounterTimer = encounters[encounterIndex].spawnTime;
        }
    }
    
    public void TriggerEncounter(EncounterInfo encounter)
    {
        Debug.Log("Encounter was triggered...");
        
        UIController.Instance.SetOverworldUI(false);
        
        CombatManager.Instance.StartCombat(PartyController.Instance.GetPartyCharacters(), encounter);
    }
    
    public void ExitCombat()
    {
        Debug.Log("Returning to the real world...");
        
        UIController.Instance.SetOverworldUI(true);

        LevelGameState = GameState.Exploring;
        
        PartyController.Instance.ReturnPartyToHolder();
        CameraController.Instance.JumpToOverworldTarget();
    }
    #endregion
    
    public void EndLevel()
    {
        Debug.Log("Put end here!");
    }
    
    public void SpawnDamageNumber(Vector3 position, int damage)
    {
        GameObject damageNumber = damageNumberPool.GetPooledObject();
        
        damageNumber.transform.position = position;
        damageNumber.GetComponent<DamageNumber>().SetDamageNumber(damage);
        
        damageNumber.SetActive(true);
    }
}
