using UnityEngine;

public class SoulDoor : PayablePopup
{
    [SerializeField]
    private Animator anim;
    
    public override void PaidAction()
    {
        anim.SetTrigger("Open");
        
        LevelManager.Instance.ClearPopupPanel();
        gameObject.SetActive(false);
    }
}
