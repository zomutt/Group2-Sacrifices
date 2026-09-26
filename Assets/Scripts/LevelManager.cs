using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    
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
    
    public static LevelManager Instance;
    
    void Awake()
    {
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        
        paymentPanel = GameObject.Find("PurchasePanel").GetComponent<PaymentPanel>();
        paymentPanel.gameObject.SetActive(false);
        
        ritualPanel = GameObject.Find("RitualPanel").GetComponent<RitualPanel>();
        ritualPanel.gameObject.SetActive(false);
        
        Instance = this;
    }
    
    void Start()
    {
        levelTime = 180f;
        
        souls = 0;
        
        encounterIndex = 0;
        encounterTimer = encounters[encounterIndex].spawnTime;
    }
    
    void Update()
    {
        if (levelTime > 0)
            levelTime -= Time.deltaTime;
        else
        {
            // Lose level
        }
        
        timerText.text = Mathf.CeilToInt(levelTime / 2f).ToString();
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
        Debug.Log("Encounter was triggered");
    }
    #endregion
}
