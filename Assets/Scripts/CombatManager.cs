using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    
    private List<CharacterBehavior> partyCharacters;
    
    [SerializeField]
    private Transform partyLineup;
    [SerializeField]
    private Transform enemyLineup;

    void Awake()
    {
        Instance = this;
    }
    
    public void StartCombat(List<CharacterBehavior> party, EncounterInfo encounter)
    {
        partyCharacters = party;
        
        PositionCharacters();
    }
    
    private void PositionCharacters()
    {
        for (int i = 0; i < partyCharacters.Count; i++)
        {
            partyCharacters[i].transform.parent = partyLineup.GetChild(i);
            partyCharacters[i].transform.localPosition = Vector3.zero;
            partyCharacters[i].DesiredRotation = -90;
        }
    }
}
