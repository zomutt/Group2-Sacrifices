using UnityEngine;

public class EndLevelTrigger : TriggerArea
{
    [SerializeField]
    private AudioClip endLevelSound;

    #region Trigger
    public override void OnEnter()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(endLevelSound);

        LevelManager.Instance.StartEndLevel();
        
        gameObject.SetActive(false);
    }

    public override void OnExit() {}

    public override void OnStay() {}
    #endregion
}
