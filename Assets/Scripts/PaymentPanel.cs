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
    [SerializeField]
    private AudioClip clickSound;
    [SerializeField]
    private AudioClip deniedSound;

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
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);

        if (!payablePopup.IsAvailable)
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySFX(deniedSound);
            return;
        }

        if (LevelManager.Instance.ConfirmSoulPayment(cost))
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySFX(paySound);

            payablePopup.PaidAction();
        }
        else
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySFX(deniedSound);
        }
    }
}
