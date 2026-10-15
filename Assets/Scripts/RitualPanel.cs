using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RitualPanel : MonoBehaviour
{
    private int reward;
    private RitualPopup ritualPopup;
    
    [SerializeField]
    private TextMeshProUGUI rewardText;
    
    [SerializeField]
    private Transform[] selectButtons;
    private Image[] selectIcons;

    [SerializeField]
    private AudioClip sacrificeSound;
    [SerializeField]
    private AudioClip clickSound;

    private CharacterBehavior[] partyCharacters;

    void Awake()
    {
        selectIcons = new Image[selectButtons.Length];
        
        for (int i = 0; i < selectButtons.Length; i++)
        {
            selectIcons[i] = selectButtons[i].GetComponent<Image>();
        }
    }

    private void UpdatePartyButtons()
    {
        partyCharacters = new CharacterBehavior[PartyController.Instance.PartySize];
        
        for (int i = 0; i < partyCharacters.Length; i++)
        {
            partyCharacters[i] = PartyController.Instance.GetPartyCharacters()[i];
            //partyHolder.GetChild(i).GetComponent<CharacterBehavior>();
            
            if (i > 0)
            {
                Debug.Log(i);
                
                selectButtons[i - 1].gameObject.SetActive(true);
                selectIcons[i - 1].sprite = PartyController.Instance.GetPartyCharacters()[i].icon; 
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
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(sacrificeSound);

        LevelManager.Instance.GainSouls(reward);
        ritualPopup.RewardAction();
        
        PartyController.Instance.SacrificePartyMember(partyIndex);
        
        UpdatePartyButtons();
    }
}
