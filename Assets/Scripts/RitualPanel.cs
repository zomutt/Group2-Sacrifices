using TMPro;
using UnityEngine;

public class RitualPanel : MonoBehaviour
{
    private int reward;
    private RitualPopup ritualPopup;
    
    [SerializeField]
    private TextMeshProUGUI rewardText;
    
    [SerializeField]
    private Transform[] selectButtons;

    [SerializeField]
    private AudioClip sacrificeSound;

    private CharacterBehavior[] partyCharacters;
    
    private void UpdatePartyButtons()
    {
        Transform partyHolder = GameObject.Find("Party").transform;
        partyCharacters = new CharacterBehavior[partyHolder.childCount];
        
        for (int i = 0; i < partyCharacters.Length; i++)
        {
            partyCharacters[i] = partyHolder.GetChild(i).GetComponent<CharacterBehavior>();
            
            if (i > 0)
            {
                selectButtons[i - 1].gameObject.SetActive(true);
                selectButtons[i - 1].GetChild(0).GetComponent<TextMeshProUGUI>().text = "hah";
            }
        }
        for (int i = partyCharacters.Length; i < 4; i++)
        {
            selectButtons[i - 1].gameObject.SetActive(false);
        }
    } 

    public void SetReward(RitualPopup popup)
    {
        ritualPopup = popup;
        reward = popup.SoulReward;
        rewardText.text = "+" + popup.SoulReward;
        
        UpdatePartyButtons();
    }
    
    public void ConfirmSacrifice(int partyIndex)
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(sacrificeSound);

        LevelManager.Instance.GainSouls(reward);
        ritualPopup.RewardAction();
        
        PartyController.Instance.SacrificePartyMember(partyIndex);
        
        UpdatePartyButtons();
    }
}
