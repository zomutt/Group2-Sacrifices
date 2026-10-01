using UnityEngine;

public class PickupTrigger : TriggerArea
{
    [SerializeField]
    private int soulValue;

    [SerializeField]
    private AudioClip pickupSound;

    #region Trigger
    public override void OnEnter()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(pickupSound);

        LevelManager.Instance.GainSouls(soulValue);
        
        gameObject.SetActive(false);
    }

    public override void OnExit() {}

    public override void OnStay() {}
    #endregion
}
