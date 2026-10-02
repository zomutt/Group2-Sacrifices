using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public enum CombatOutcome
    {
        Ongoing,
        PartyWin,
        EnemyWin
    }

    private List<CharacterBehavior> partyCharacters;
    private List<CharacterBehavior> enemyCharacters;
    
    [SerializeField]
    private Transform partyLineup;
    [SerializeField]
    private Transform enemyLineup;
    
    private float playerTurnTimer;
    public float PlayerTurnTimer => playerTurnTimer;
    private int playerTurnTriggerIndex;
    
    private GameObject combatUI;

    [SerializeField]
    private AudioClip battleStartSound;
    
    [HideInInspector]
    public CharacterBehavior pinnedTarget;

    public static float CombatSpeed = 0.5f;
    public static CombatManager Instance;

    void Awake()
    {
        Instance = this;

        partyCharacters = new();
        enemyCharacters = new();
        
        combatUI = GameObject.Find("CombatOnly");
    }

    private void Start()
    {
        UIController.Instance.EndBattleUI();
        
        combatUI.SetActive(false);
    }

    void Update()
    {
        if (LevelManager.LevelGameState == GameState.Battle)
            TickBattle();
    }

    public void StartCombat(List<CharacterBehavior> party, EncounterInfo encounter)
    {
        LevelManager.LevelGameState = GameState.Battle;

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(battleStartSound);

        partyCharacters = party;
        PositionCharacters();
        
        SpawnEnemyCharacters(encounter);
        
        // The player really does not need this info
        // during a battle so I am putting this on hold
        // for now because it covers the whole screen :,)
        //UIController.Instance.StartBattleUI();
        
        combatUI.SetActive(true);
        
        playerTurnTimer = 0;
        playerTurnTriggerIndex = 0;
        
        SetPinnedTargetDefault();
    }

    #region Character Management
    private void PositionCharacters()
    {
        for (int i = 0; i < partyCharacters.Count; i++)
        {
            partyCharacters[i].transform.parent = partyLineup.GetChild(i);
            partyCharacters[i].transform.localPosition = Vector3.zero;
            partyCharacters[i].DesiredRotation = -90;
            partyCharacters[i].ImmedatelyReorient();
        }
    }
    
    private void SpawnEnemyCharacters(EncounterInfo encounter)
    {
        GameObject[] enemies = encounter.enemyPrefabs;
        enemyCharacters = new();
                
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                GameObject newEnemy = Instantiate(enemies[i], enemyLineup.GetChild(i));
                newEnemy.transform.localPosition = Vector3.zero;
                
                CharacterBehavior enemyChara = newEnemy.GetComponent<CharacterBehavior>();
                enemyChara.DesiredRotation = 90;
                enemyChara.SetEnemyTurnTimer(2 - i);
                enemyChara.isEnemy = true;

                enemyCharacters.Add(enemyChara);
            }
        }
    }

    public void RemoveFromBattle(CharacterBehavior toRemove)
    {
        if (partyCharacters.Contains(toRemove))
        {
            partyCharacters.Remove(toRemove);
        }
        else if (enemyCharacters.Contains(toRemove))
        {
            enemyCharacters.Remove(toRemove);
        }
        
        if (pinnedTarget == toRemove)
            SetPinnedTargetDefault();
    }
    #endregion

    private void TickBattle()
    {
        if (TryEndBattle())
            return;

        playerTurnTimer += Time.deltaTime * CombatSpeed;
        
        if (Mathf.FloorToInt(playerTurnTimer) > playerTurnTriggerIndex)
        {
            DoPlayerTurn(playerTurnTriggerIndex);
        }
    }

    #region End of Battle
    private bool TryEndBattle()
    {
        switch (CheckRemainingAlive())
        {
            case CombatOutcome.PartyWin:
                Debug.Log("Battle won!");
                EndActiveCombat();
                Invoke("LeaveBattle", 2f);
                return true;

            case CombatOutcome.EnemyWin:
                Debug.Log("Battle lost!");
                EndActiveCombat();
                Invoke("LoseBattle", 1f);
                return true;

            default:
                break;
        }

        return false;
    }

    private CombatOutcome CheckRemainingAlive()
    {
        if (partyCharacters.Count == 0)
            return CombatOutcome.EnemyWin;
        else if (enemyCharacters.Count == 0)
            return CombatOutcome.PartyWin;
        else
            return CombatOutcome.Ongoing;
    }
    
    private void EndActiveCombat()
    {
        LevelManager.LevelGameState = GameState.EndOfBattle;
    }

    private void LeaveBattle()
    {
        UIController.Instance.EndBattleUI();
        combatUI.SetActive(false);

        LevelManager.Instance.ExitCombat();
    }

    private void LoseBattle()
    {
        UIController.Instance.ShowLosePanel();
    }
    #endregion
    
    #region Getting Targets
    public CharacterBehavior GetRandomTarget(bool onEnemyTeam)
    {
        if (onEnemyTeam)
        {
            return enemyCharacters[UnityEngine.Random.Range(0, enemyCharacters.Count)];
        }
        else
        {
            return partyCharacters[UnityEngine.Random.Range(0, partyCharacters.Count)];
        }
    }
    
    public CharacterBehavior GetLowestHealthTarget(bool onEnemyTeam)
    {
        List<CharacterBehavior> teamCharacters =
            (onEnemyTeam) ? enemyCharacters : partyCharacters;
        
        float healthPercent = teamCharacters[0].Stats.HealthPercent;
        int lowestHealthIndex = 0;
        
        for (int i = 1; i < teamCharacters.Count; i++)
        {
            if (healthPercent > teamCharacters[i].Stats.HealthPercent)
            {
                healthPercent = teamCharacters[i].Stats.HealthPercent;
                lowestHealthIndex = i;
            }
        }
        
        return teamCharacters[lowestHealthIndex];
    }
    
    private void SetPinnedTargetDefault()
    {
        if (enemyCharacters.Count > 0)
            pinnedTarget = enemyCharacters[0];
    }
    #endregion

    private void DoPlayerTurn(int index)
    {
        if (index < partyCharacters.Count && partyCharacters[index] != null)
            partyCharacters[index].DoTurn();

        // Progress turn index
        playerTurnTriggerIndex = (index + 1) % 4;
        playerTurnTimer = playerTurnTriggerIndex;
    }

    public void SkipToTurn(int toTurn)
    {
        int breakCheck = 0;
        List<CharacterBehavior> skippedBehaviors = new();

        if (partyCharacters[playerTurnTriggerIndex])
            skippedBehaviors.Add(partyCharacters[playerTurnTriggerIndex]);

        while (playerTurnTriggerIndex != toTurn)
        {
            playerTurnTriggerIndex = (playerTurnTriggerIndex + 1) % 4;

            if (partyCharacters[playerTurnTriggerIndex])
                skippedBehaviors.Add(partyCharacters[playerTurnTriggerIndex]);

            #region Break Loop
            breakCheck++;
            if (breakCheck >= 8)
            {
                Debug.LogError("SKIP TURN LOOP ERROR");
                return;
            }
            #endregion
        }

        foreach (CharacterBehavior behavior in skippedBehaviors)
        {
            behavior.TakeDamage(12 / skippedBehaviors.Count);
        }

        playerTurnTimer = playerTurnTriggerIndex + 1;
        
        Debug.Log($"Skipped {skippedBehaviors.Count} turns.");
    }
}
