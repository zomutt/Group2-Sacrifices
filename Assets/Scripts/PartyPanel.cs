using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyPanel : MonoBehaviour
{
    [SerializeField]
    private Slider[] healthSliders;

    void Update()
    {
        UpdateHealthbars();
    }
    
    private void UpdateHealthbars()
    {
        if (PartyController.Instance == null) // Not sure if this could happen but ye
            return;
        
        List<CharacterBehavior> characters = PartyController.Instance.GetPartyCharacters();
        
        for (int i = 0; i < characters.Count; i++)
        {
            SetHealthbarValue(healthSliders[i], characters[i]);
        }
    }
    
    private void SetHealthbarValue(Slider slider, CharacterBehavior character)
    {
        slider.value = character.Stats.HealthPercent;
    }
}
