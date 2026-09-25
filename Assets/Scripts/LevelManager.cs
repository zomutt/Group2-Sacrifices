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
    private GameObject paymentPanel;
    
    public static LevelManager Instance;
    
    void Awake()
    {
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        
        paymentPanel = GameObject.Find("PurchasePanel");
        
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
        {
            // End level, lose
        }
        else
            levelTime -= Time.deltaTime;
        
        timerText.text = Mathf.CeilToInt(levelTime / 2f).ToString();
    }
    
    public bool RequestTakeSouls(int amount)
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
    
    public void UpdatePaymentPanel()
    {
        if (currentPayable == null)
        {
            paymentPanel.SetActive(false);
        }
        else
        {
            paymentPanel.SetActive(true);
        }
    }
}
