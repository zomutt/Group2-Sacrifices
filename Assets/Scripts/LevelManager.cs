using NUnit.Framework;
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
    [HideInInspector]
    public float afterEncounterTimer;
    
    [SerializeField]
    private AudioClip soulGainSound;
    [SerializeField]
    private AudioClip popupOpenSound;
    [SerializeField]
    private AudioClip popupCloseSound;

    private ObjectPooler damageNumberPool;
    
    public static GameState LevelGameState;
    public static LevelManager Instance;
    
    void Awake()
    {
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

        levelTime = 270f;
        
        souls = 0;
        
        encounterIndex = 0;
        encounterTimer = encounters[encounterIndex].spawnTime;
        
        CameraController.Instance.JumpToOverworldTarget();
    }
    
    void Update()
    {
        if (LevelGameState != GameState.Cutscene) TickLevelTimer();
        
        UpdateUI();
    }
    
    private void TickLevelTimer()
    {
        if (levelTime > 0)
            levelTime -= Time.deltaTime;
        else
        {
            UIController.Instance.ShowLosePanel();
        }
        
        if (afterEncounterTimer > 0)
            afterEncounterTimer -= Time.deltaTime;
    }
    
    private void UpdateUI()
    {
        UIController.Instance.SetTimer(Mathf.CeilToInt(levelTime / 3f));

        UIController.Instance.SetSouls(souls);
    }
    
    #region Souls
    public void GainSouls(int addedSouls)
    {
        souls += addedSouls;

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(soulGainSound);
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

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(popupOpenSound);
    }
    
    public void SetSacrificePanel(RitualPopup popup)
    {
        currentPopup = popup;
        
        currentPopupPanel = ritualPanel.gameObject;
        
        ritualPanel.gameObject.SetActive(true);
        ritualPanel.SetReward(popup);

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(popupOpenSound);
    }
    
    public void ClearPopupPanel()
    {
        currentPopup = null;
        currentPopupPanel.SetActive(false);

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(popupCloseSound);
    }
    #endregion
    
    #region Encounters
    public void ProgressEncounterTimer(float time)
    {
        if (skipEncounters || afterEncounterTimer > 0)
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
        
        afterEncounterTimer = 8f;
    }
    #endregion
    
    public void EndLevel()
    {
        Debug.Log("Put end here!");

        UIController.Instance.ShowWinPanel();
    }
    
    public void SpawnDamageNumber(Vector3 position, int damage)
    {
        GameObject damageNumber = damageNumberPool.GetPooledObject();
        
        damageNumber.transform.position = position;
        damageNumber.GetComponent<DamageNumber>().SetDamageNumber(damage);
        
        damageNumber.SetActive(true);
    }
}
