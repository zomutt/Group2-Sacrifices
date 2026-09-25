using System.Diagnostics;
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
    private PaymentPanel paymentPanel;
    private GameObject currentPopupPanel;
    
    public static LevelManager Instance;
    
    void Awake()
    {
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        
        paymentPanel = GameObject.Find("PurchasePanel").GetComponent<PaymentPanel>();
        paymentPanel.gameObject.SetActive(false);
        
        
        
        Instance = this;
    }
    
    void Start()
    {
        levelTime = 180f;
        
        souls = 100;
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
    
    #region Soul Payments
    public bool ConfirmPayment(int amount)
    {
        if (souls >= amount)
        {
            souls -= amount;
            currentPopup.GetComponent<PayablePopup>().PaidAction();
            return true;
        }
        else
            return false;
    }
    
    public void SetPaymentPanel(PayablePopup popup)
    {
        currentPopup = popup;
        
        currentPopupPanel = paymentPanel.gameObject;
        
        paymentPanel.SetCost(popup.SoulCost);
    }
    #endregion
    
    #region Sacrifices
    public void SetSacrificePanel(RitualPopup popup)
    {
        currentPopup = popup;
        
        currentPopupPanel = paymentPanel.gameObject;
    }
    #endregion
    
    public void ClearPopupPanel()
    {
        currentPopup = null;
        currentPopupPanel.SetActive(false);
    }
}
