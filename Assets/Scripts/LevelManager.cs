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
    
    public PopupTrigger currentPopup;
    private GameObject currentPopupPanel;
    
    private PaymentPanel paymentPanel;
    private RitualPanel ritualPanel;
    
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
    }
    
    void Update()
    {
        if (levelTime > 0)
            levelTime -= Time.deltaTime;
        else
        {
            // End level, lose
        }
        
        timerText.text = Mathf.CeilToInt(levelTime / 2f).ToString();
    }
    
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
}
