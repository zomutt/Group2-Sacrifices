using UnityEngine;

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
    
    private int health;
    [SerializeField]
    private int maxHealth;
    
    #region In Battle
    private bool amOnEnemyTeam;
    
    [SerializeField, Tooltip("For use as enemy")]
    private float maxTurnTimer;
    private float personalTurnTimer;
    #endregion

    void Start()
    {
        health = maxHealth;
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
        // Remove from party controller
        PartyController.Instance.RemoveFromParty(this);
        
        // Remove from party holder and hide
        transform.parent = transform.parent.parent;
        gameObject.SetActive(false);
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
        
        // To be implemented
    }
    #endregion
    
}
