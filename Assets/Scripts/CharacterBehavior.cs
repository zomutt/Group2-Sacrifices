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
    
    [HideInInspector]
    public int addedArmor;
    
    [HideInInspector]
    public int addedStrength;
    
    [SerializeField]
    private int soulsOnDeath;
    
    private CharacterBehavior currentTarget;
    [SerializeField]
    private List<AttackBehavior> attacks;
    
    public List<StatusEffect> statuses;
    public List<StatusEffect> statusesToRemove;

    [SerializeField]
    private AudioClip hurtSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip soulDropSound;
    
    private bool damageNumberWanted;
    private int damageNumberValue;

    #region In Battle
    [SerializeField, Tooltip("For use as enemy")]
    private float maxTurnTimer;
    private float personalTurnTimer;
    
    public bool isEnemy;
    
    private bool isHoldingTurn;
    
    [HideInInspector]
    public StatusDisplay statusDisplay;
    
    [SerializeField, Tooltip("For use as enemy")]
    private EnemyBrain brain;
    #endregion

    void Start()
    {
        stats.RefillHealth();
    }

    void Update()
    {
        ModelOrientation();
        
        TickTurn();
        
        if (damageNumberWanted)
            SpawnDamageNumber();
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
    
    public void DoTurn()
    {
        DoTurnStart();
        
        if (isEnemy)
            DoMove(Random.Range(0, attacks.Count));
        else
        {
            MovePanel.Instance.OpenPanel(attacks, this);
        }
    }
    
    public void DoMove(int index)
    {
        if (index == 3 && !isEnemy) // This is a hack solution to make the mega attack cost souls
        {
            LevelManager.Instance.ConfirmSoulPayment(LevelManager.Instance.Souls / 2);
        }
        
        AttackBehavior usedAttack = attacks[index];
        Debug.Log($"{gameObject.name} used {usedAttack.attackName}");
        usedAttack.MoveOnTarget(this, GetTargetForMove(usedAttack));
        
        if (!isEnemy)
            CombatManager.Instance.EndPlayerTurn();
    }
    
    private void DoTurnStart()
    {
        addedStrength = 0;
        
        foreach (StatusEffect status in statuses)
        {
            status.DoTurnStart(this);
        }
        
        isHoldingTurn = true;
    }
    
    private CharacterBehavior GetTargetForMove(AttackBehavior move)
    {
        if (move.targetType.targetsSelf)
            return this;
        
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
    
    public void EndOfBattle()
    {
        // Clear statuses
        for (int i = 0; i < statuses.Count; i++)
        {
            if (!statuses[i].permanent)
            {
                statusDisplay.RemoveStatus(statuses[i]);
                statuses.RemoveAt(i);
                i--;
            }
        }
        
        damageNumberWanted = false;
        damageNumberValue = 0;
    }
    #endregion
    
    #region Damage Taking
    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        
        AddToDamageNumber(-damage);

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(hurtSound);
        
        if (!stats.HasHealthRemaining)
        {
            OnDeath();
            
            SpawnDamageNumber();
        }
    }
    
    public void GainHealth(int health)
    {
        stats.health = Mathf.Clamp(stats.health + health, 0, stats.maxHealth);
        
        AddToDamageNumber(health);
    }

    private void OnDeath()
    {
        Debug.Log(gameObject.name + " died");

        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(deathSound);
        
        PartyController.Instance.RemoveFromParty(this);
        CombatManager.Instance.RemoveFromBattle(this);
        
        if (isEnemy)
        {
            LevelManager.Instance.GainSouls(soulsOnDeath);
            if (AudioManager.Instance) AudioManager.Instance.PlaySFX(soulDropSound);
        }
        
        if (isHoldingTurn)
            CombatManager.Instance.EndPlayerTurn();
        
        transform.parent = transform.root;
        gameObject.SetActive(false);
    }
    
    private void AddToDamageNumber(int numberValue)
    {
        damageNumberWanted = true;
        damageNumberValue += numberValue;
    }
    
    private void SpawnDamageNumber()
    {
        LevelManager.Instance.SpawnDamageNumber(transform.position + Vector3.up, damageNumberValue);
        
        damageNumberWanted = false;
        damageNumberValue = 0;
    }
    #endregion
    
    #region Status Effects
    private void TickStatusEffects()
    {
        foreach (StatusEffect status in statuses)
        {
            status.TickStatusEffect(this);
        }
        
        if (statusesToRemove.Count > 0)
        {
            foreach (StatusEffect status in statusesToRemove)
            {
                status.DoOnEnd(this);
                
                statuses.Remove(status);
                statusDisplay.RemoveStatus(status);
            }
            statusesToRemove.Clear();
        }
    }
    
    public void AddStatusEffect(StatusEffect effect)
    {
        statuses.Add(effect);
        statusDisplay.AddStatus(effect);
        
        effect.DoOnAdd(this);
    }
    
    public void DoOnHurtMelee(CharacterBehavior attacker)
    {
        foreach (StatusEffect status in statuses)
        {
            status.DoOnHurtMelee(this, attacker);
        }
    }
    #endregion
    
}
