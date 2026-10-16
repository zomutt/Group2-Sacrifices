using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBars : MonoBehaviour
{
    [SerializeField]
    private Slider[] partyHealth;
    [SerializeField]
    private Slider[] enemyHealth;
    [SerializeField]
    private Slider[] enemyCharge;

    void Update()
    {
        UpdateHealthbars();
    }

    private void UpdateHealthbars()
    {
        UpdateGroup(partyHealth, CombatManager.Instance.partyOrdered);
        UpdateGroup(enemyHealth, CombatManager.Instance.enemyOrdered);
        UpdateGroup(enemyCharge, CombatManager.Instance.enemyOrdered, true);
    }
    
    private void UpdateGroup(Slider[] group, List<CharacterBehavior> characters, bool isChargeBar = false)
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
                
                if (isChargeBar)
                    SetChargebarValue(group[i], characters[i]);
                else
                    SetHealthbarValue(group[i], characters[i]);
            }
        }
    }
    
    private void SetHealthbarValue(Slider slider, CharacterBehavior character)
    {
        slider.value = character.Stats.HealthPercent;
    }
    
    private void SetChargebarValue(Slider slider, CharacterBehavior character)
    {
        slider.value = character.ChargePercent;
    }
}
