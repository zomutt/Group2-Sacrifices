using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private List<CharacterBehavior> partyCharacters;
    
    [SerializeField]
    private Transform partyLineup;
    [SerializeField]
    private Transform enemyLineup;
    
    private float playerTurnTimer;
    private int playerTurnTriggerIndex;
    
    public static float CombatSpeed = 0.5f;
    public static CombatManager Instance;
    
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (LevelManager.LevelGameState == GameState.Battle)
            TickBattle();
    }

    public void StartCombat(List<CharacterBehavior> party, EncounterInfo encounter)
    {
        LevelManager.LevelGameState = GameState.Battle;
        
        partyCharacters = party;
        
        PositionCharacters();
        
        SpawnEnemyCharacters(encounter);
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
    
    private void SpawnEnemyCharacters(EncounterInfo encounter)
    {
        GameObject[] enemies = encounter.enemyPrefabs;
                
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                GameObject newEnemy = Instantiate(enemies[i], enemyLineup.GetChild(i));
                newEnemy.transform.localPosition = Vector3.zero;
                
                CharacterBehavior enemyChara = newEnemy.GetComponent<CharacterBehavior>();
                enemyChara.DesiredRotation = 90;
                enemyChara.SetEnemyTurnTimer(2 - i);
            }
        }
    }
    
    private void TickBattle()
    {
        playerTurnTimer += Time.deltaTime * CombatSpeed;
        
        if (Mathf.FloorToInt(playerTurnTimer) > playerTurnTriggerIndex)
        {
            DoPlayerTurn(playerTurnTriggerIndex);
        }
    }
    
    private void DoPlayerTurn(int index)
    {
        if (partyCharacters.Count >= index && partyCharacters[index] != null)
            partyCharacters[index].DoTurn();
        
        // Progress turn index
        playerTurnTriggerIndex = (index + 1) % 4;
        playerTurnTimer = playerTurnTriggerIndex;
    }
}
