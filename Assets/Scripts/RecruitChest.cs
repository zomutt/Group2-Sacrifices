using UnityEngine;

public class RecruitChest : PayablePopup
{
    [SerializeField]
    private GameObject lockedCharacter;

    [SerializeField]
    private AudioClip openSound;

    public override bool IsAvailable
        => PartyController.Instance.PartySize < 4;
    public override string UnavailableText
        => "Party Full";
    
    public override void PaidAction()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(openSound);

        lockedCharacter.SetActive(true);
        lockedCharacter.GetComponent<CharacterBehavior>().AddToParty();
        
        LevelManager.Instance.ClearPopupPanel();
        gameObject.SetActive(false);
    }
}
