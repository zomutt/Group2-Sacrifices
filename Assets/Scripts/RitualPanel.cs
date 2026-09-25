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
    
    private CharacterBehavior[] partyCharacters;

    void OnEnable()
    {
        UpdatePartyButtons();
    }

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
    }
    
    public void ConfirmSacrifice(int partyIndex)
    {
        LevelManager.Instance.GainSouls(reward);
        ritualPopup.RewardAction();
        
        UpdatePartyButtons();
    }
}
