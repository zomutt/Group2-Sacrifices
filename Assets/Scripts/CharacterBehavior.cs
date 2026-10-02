using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform modelPivot;
    
    private float desiredRot;
    public float DesiredRotation
    {
        set => desiredRot = value;
    }
    
    private readonly float pivotRotateSpeed = 100f;
    
    [SerializeField]
    private CharacterStats stats;
    public CharacterStats Stats
        => stats;
    
    [SerializeField]
    private int soulsOnDeath;
    
    private CharacterBehavior currentTarget;
    [SerializeField]
    private AttackBehavior attack;
    
    [SerializeField]
    private List<StatusEffect> statuses;

    [SerializeField]
    private AudioClip hurtSound;
    [SerializeField]
    private AudioClip deathSound;

    #region In Battle
    [SerializeField, Tooltip("For use as enemy")]
    private float maxTurnTimer;
    private float personalTurnTimer;
    
    public bool isEnemy;
    #endregion

    void Start()
    {
        stats.RefillHealth();
    }

    void Update()
    {
        ModelOrientation();
        
        TickTurn();
    }
    
    #region Rotation
    private void ModelOrientation()
    {
        if (LevelManager.LevelGameState == GameState.Exploring)
            RotatePivot(desiredRot, pivotRotateSpeed * Time.deltaTime * PartyController.Instance.CurrentVelocity);
        else
            RotatePivot(desiredRot, pivotRotateSpeed * Time.deltaTime * 6f);
    }

    public void ImmedatelyReorient()
    {
        RotatePivot(desiredRot, 180f);
    }
    
    void RotatePivot(float toRotation, float maxAmount)
    {
        float difference = ((toRotation - transform.rotation.eulerAngles.y) % 360 + 540) % 360 - 180;
        float newRotation = transform.rotation.eulerAngles.y;
        
        if (Mathf.Abs(difference) <= maxAmount)
        {
            newRotation = toRotation;
        }
        else
        {
            float normalizedDiff = difference / Mathf.Abs(difference);
            
            newRotation += normalizedDiff * maxAmount;
        }
        
        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
    #endregion
    
    #region Party Management
    public void AddToParty()
    {
        PartyController.Instance.AddPartyMember(this);
    }
    
    public void Sacrifice()
    {
        OnDeath();
    }
    #endregion
    
    #region Turn Management
    public void SetEnemyTurnTimer(int fullness)
    {
        isEnemy = true;
        
        personalTurnTimer = maxTurnTimer / 4f * fullness + 0.5f;
    }
    
    private void TickTurn()
    {
        if (LevelManager.LevelGameState != GameState.Battle)
            return;
        
        if (isEnemy)
        {
            TickEnemyTurn();
        }
        
        TickStatusEffects();
    }
    
    private void TickEnemyTurn()
    {
        personalTurnTimer += Time.deltaTime * CombatManager.CombatSpeed;
        
        if (personalTurnTimer >= maxTurnTimer)
        {
            personalTurnTimer -= maxTurnTimer;
            
            DoTurn();
        }
    }
    
    private void TickStatusEffects()
    {
        foreach (StatusEffect status in statuses)
        {
            status.TickStatusEffect(this);
        }
    }
    
    public void DoTurn()
    {
        Debug.Log(gameObject.name + " did turn");
        
        DoTurnStart();

        attack.MoveOnTarget(this, GetTargetForMove(attack));
    }
    
    private void DoTurnStart()
    {
        foreach (StatusEffect status in statuses)
        {
            status.DoTurnStart(this);
        }
    }
    
    private CharacterBehavior GetTargetForMove(AttackBehavior move)
    {
        bool targetsEnemies = move.targetType.targetsEnemies;
        
        if (!isEnemy) // Party tries to target the pinned enemy
        {
            bool pinnedIsEnemy = CombatManager.Instance.pinnedTarget.isEnemy;
            
            if (pinnedIsEnemy == targetsEnemies) // If my move can target pinned enemy
                return CombatManager.Instance.pinnedTarget;
        }
        
        if (!targetsEnemies) // Tries to help lowest health ally
            return CombatManager.Instance.GetLowestHealthTarget(isEnemy);
        
        // Given nothing else, characters target randomly
        return CombatManager.Instance.GetRandomTarget(!isEnemy);
    }
    #endregion
    
    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        
        LevelManager.Instance.SpawnDamageNumber(transform.position + Vector3.up, damage);

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(hurtSound);

        if (!stats.HasHealthRemaining)
        {
            OnDeath();
        }
    }
    
    public void GainHealth(int health)
    {
        stats.health = Mathf.Clamp(stats.health + health, 0, stats.maxHealth);
        
        LevelManager.Instance.SpawnDamageNumber(transform.position + Vector3.up, health, true);
    }

    private void OnDeath()
    {
        Debug.Log(gameObject.name + " died");

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(deathSound);

        PartyController.Instance.RemoveFromParty(this);
        CombatManager.Instance.RemoveFromBattle(this);
        
        if (isEnemy)
            LevelManager.Instance.GainSouls(soulsOnDeath);
        
        transform.parent = transform.root;
        gameObject.SetActive(false);
    }
}
