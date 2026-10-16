using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBars : MonoBehaviour
{
    [SerializeField]
    private Slider[] partyHealth;
    [SerializeField]
    private Slider[] enemyHealth;

    void Update()
    {
        UpdateHealthbars();
    }

    private void UpdateHealthbars()
    {
        UpdateGroup(partyHealth, CombatManager.Instance.partyOrdered);
        UpdateGroup(enemyHealth, CombatManager.Instance.enemyOrdered);
    }
    
    private void UpdateGroup(Slider[] group, List<CharacterBehavior> characters)
    {
        for (int i = 0; i < group.Length; i++)
        {
            if (characters[i] == null)
            {
                group[i].gameObject.SetActive(false);
            }
            else
            {
                group[i].gameObject.SetActive(true);
                SetHealthbarValue(group[i], characters[i]);
            }
        }
    }
    
    private void SetHealthbarValue(Slider slider, CharacterBehavior character)
    {
        slider.value = character.Stats.HealthPercent;
    }
}
