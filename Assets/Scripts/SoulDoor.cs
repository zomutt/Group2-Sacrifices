using UnityEngine;

public class SoulDoor : PayableTrigger
{
    [SerializeField]
    private Animator anim;
    
    public override void PaidAction()
    {
        anim.SetTrigger("Open");
    }
}
