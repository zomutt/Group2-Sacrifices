using TMPro;
using UnityEngine;

public class PaymentPanel : MonoBehaviour
{
    private int cost;
    private PayablePopup payablePopup;
    
    [SerializeField]
    private TextMeshProUGUI costText;
    
    public void SetCost(PayablePopup popup)
    {
        payablePopup = popup;
        cost = popup.SoulCost;
        costText.text = "-" + popup.SoulCost;
    }
    
    public void ConfirmPayment()
    {
        if (LevelManager.Instance.ConfirmSoulPayment(cost))
            payablePopup.PaidAction();
    }
}
