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
    
    public PayableTrigger currentPayable;
    private PaymentPanel paymentPanel;
    
    public static LevelManager Instance;
    
    void Awake()
    {
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        
        paymentPanel = GameObject.Find("PurchasePanel").GetComponent<PaymentPanel>();
        
        Instance = this;
    }
    
    void Start()
    {
        levelTime = 180f;
        
        souls = 100;
        
        SetPaymentPanel(null);
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
            currentPayable.PaidAction();
            return true;
        }
        else
            return false;
    }
    
    public void SetPaymentPanel(PayableTrigger payable)
    {
        currentPayable = payable;
        if (currentPayable == null)
        {
            paymentPanel.gameObject.SetActive(false);
        }
        else
        {
            paymentPanel.gameObject.SetActive(true);
            paymentPanel.SetCost(currentPayable.SoulCost);
        }
    }
    #endregion
}
