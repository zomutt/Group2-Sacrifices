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
    
    #region In Battle
    private bool amOnEnemyTeam;
    
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
        amOnEnemyTeam = true;
        
        personalTurnTimer = maxTurnTimer / 4f * fullness + 0.5f;
    }
    
    private void TickTurn()
    {
        if (LevelManager.LevelGameState != GameState.Battle)
            return;
        
        if (amOnEnemyTeam)
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
        // To be implemented
    }
    
    public void DoTurn()
    {
        Debug.Log(gameObject.name + " did turn");

        attack.MoveOnTarget(this, CombatManager.Instance.RandomTarget(!amOnEnemyTeam));
    }
    #endregion
    
    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        
        LevelManager.Instance.SpawnDamageNumber(transform.position + Vector3.up, damage);

        if (!stats.HasHealthRemaining)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log(gameObject.name + " died");

        PartyController.Instance.RemoveFromParty(this);
        CombatManager.Instance.RemoveFromBattle(this);
        
        if (isEnemy)
            LevelManager.Instance.GainSouls(soulsOnDeath);
        
        transform.parent = transform.root;
        gameObject.SetActive(false);
    }
}
