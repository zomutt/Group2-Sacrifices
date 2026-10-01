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

    [SerializeField]
    private AudioClip paySound;

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
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySFX(paySound);

            payablePopup.PaidAction();
        }
    }
}
