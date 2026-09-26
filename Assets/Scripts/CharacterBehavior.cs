using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public void AddToParty()
    {
        PartyController.Instance.AddPartyMember(this);
    }
    
    public void Sacrifice()
    {
        // Remove from party controller
        PartyController.Instance.RemoveFromParty(this);
        
        // Remove from party holder and hide
        transform.parent = transform.parent.parent;
        gameObject.SetActive(false);
    }
}
