using TMPro;
using UnityEngine;

public class PaymentPanel : MonoBehaviour
{
    private int cost;
    
    [SerializeField]
    private TextMeshProUGUI costText;
    
    public void SetCost(int newCost)
    {
        cost = newCost;
        costText.text = "-" + newCost;
    }
    
    public void ConfirmPayment()
    {
        LevelManager.Instance.ConfirmPayment(cost);
    }
}
