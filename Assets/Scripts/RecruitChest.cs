using UnityEngine;

public class RecruitChest : PayablePopup
{
    [SerializeField]
    private GameObject lockedCharacter;
    
    public override bool IsAvailable
        => PartyController.Instance.PartySize < 4;
    public override string UnavailableText
        => "Party Full";
    
    public override void PaidAction()
    {
        lockedCharacter.SetActive(true);
        lockedCharacter.GetComponent<CharacterBehavior>().AddToParty();
        
        LevelManager.Instance.ClearPopupPanel();
        gameObject.SetActive(false);
    }
}
