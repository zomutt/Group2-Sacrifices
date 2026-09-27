using TMPro;
using UnityEngine;

public class PaymentPanel : MonoBehaviour
{
    private int cost;
    private PayablePopup payablePopup;
    
    [SerializeField]
    private TextMeshProUGUI costText;
    
    [SerializeField]
    private GameObject confirmButton;
    
    public void SetCost(PayablePopup popup)
    {
        payablePopup = popup;
        cost = popup.SoulCost;
        
        if (popup.IsAvailable)
        {
            costText.text = "-" + popup.SoulCost;
            confirmButton.SetActive(LevelManager.Instance.Souls >= popup.SoulCost);
        }
        else
        {
            costText.text = popup.UnavailableText;
            confirmButton.SetActive(false);
        }
    }
    
    public void ConfirmPayment()
    {
        if (!payablePopup.IsAvailable)
            return;
        
        if (LevelManager.Instance.ConfirmSoulPayment(cost))
            payablePopup.PaidAction();
    }
}
