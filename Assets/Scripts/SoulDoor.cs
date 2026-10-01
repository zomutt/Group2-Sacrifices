using UnityEngine;

public class SoulDoor : PayablePopup
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioClip openSound;

    public override void PaidAction()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(openSound);

        anim.SetTrigger("Open");
        
        LevelManager.Instance.ClearPopupPanel();
        gameObject.SetActive(false);
    }
}
