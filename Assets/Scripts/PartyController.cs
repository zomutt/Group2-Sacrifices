using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyController : MonoBehaviour
{
    private InputSystem_Actions controls;
    
    private Vector2 moveDir;
    private Vector3 lastMoveDir;
    [SerializeField]
    private float moveSpeed;
    public float CurrentVelocity
        => moveDir.magnitude * moveSpeed;
    
    private Rigidbody rb;
    
    [SerializeField]
    private List<CharacterBehavior> characters;
    
    public int PartySize
    {
        get => characters.Count;
    }
    
    private List<Vector3> trail = new();
    private float pullAmount;
    private readonly float trailStep = 0.25f;
    
    private Transform partyHolder;
    
    public static PartyController Instance;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        controls = new InputSystem_Actions();
        
        Instance = this;
        
        partyHolder = GameObject.Find("Party").transform;
    }
    
    void Start()
    {
        ChangeTrailLength(Mathf.CeilToInt(characters.Count / trailStep) + 1);
    }
    
    #region ControlsEnableDisable
    void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += SetMoveDir;
        controls.Player.Move.canceled += SetMoveDir;
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= SetMoveDir;
        controls.Player.Move.canceled -= SetMoveDir;
        controls.Disable();
    }
    #endregion
    
    void Update()
    {
        if (LevelManager.LevelGameState == GameState.Exploring)
        {
            Move();
            
            PullTrail();
        }
        else
        {
            moveDir = Vector2.zero;
            rb.linearVelocity = Vector3.zero;
        }
    }
    
    #region Movement
    private void Move()
    {
        rb.linearVelocity = new Vector3(moveDir.x, 0, moveDir.y) * moveSpeed;
        
        if (rb.linearVelocity.magnitude > 0)
        {
            lastMoveDir = rb.linearVelocity;
        }
    }
    
    private void SetMoveDir(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>().normalized;
    }
    #endregion
    
    #region Party Management
    public void AddPartyMember(CharacterBehavior character)
    {
        characters.Add(character);
        character.transform.parent = partyHolder.transform;
        ChangeTrailLength(Mathf.CeilToInt(characters.Count / trailStep) + 1);
    }
    
    public void SacrificePartyMember(int index)
    {
        characters[index].Sacrifice();
    }
    
    public void RemoveFromParty(CharacterBehavior character)
    {
        characters.Remove(character);
        ChangeTrailLength(Mathf.CeilToInt(characters.Count / trailStep) + 1);
    }
    
    public List<CharacterBehavior> GetPartyCharacters()
    {
        return characters;
    }
    #endregion
    
    #region Trail
    private void PullTrail()
    {
        while (VectTrailToPrevTrail(0).magnitude > trailStep)
        {
            trail.Insert(0, trail[0] + VectTrailToPrevTrail(0).normalized * trailStep);
            trail.RemoveAt(trail.Count - 1);
        }
        
        pullAmount = VectTrailToPrevTrail(0).magnitude;
        
        characters[0].transform.position = transform.position;
        characters[0].DesiredRotation = DirectionForModel(lastMoveDir);
        
        for (int i = 1; i < characters.Count; i++)
        {
            float temp = i / trailStep - pullAmount;
            
            int index = Mathf.CeilToInt(temp);
            float remainder = (temp % 1 + 1) % 1;
            
            characters[i].transform.position = trail[index] + VectTrailToPrevTrail(index).normalized * pullAmount;
            characters[i].DesiredRotation = DirectionForModel(VectTrailToPrevTrail(index));
        }
    }
    
    private Vector3 VectTrailToPrevTrail(int index)
    {
        if (index <= 0)
            return transform.position - trail[0];
        else
            return trail[index - 1] - trail[index];
    }
    
    private float DirectionForModel(Vector3 facingVector)
    {
        return Vector3.SignedAngle(Vector3.forward, facingVector, Vector3.up);
    }

    private void ChangeTrailLength(int length)
    {
        if (length > trail.Count)
        {
            for (int i = trail.Count; i < length; i++)
            {
                if (i == 0)
                    trail.Add(transform.position);
                else
                    trail.Add(trail[i - 1]);
            }
        }
        else
        {
            for (int i = trail.Count; i > length; i--)
            {
                trail.RemoveAt(i - 1);
            }
        }
    }
    #endregion
    
    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TriggerArea>().OnEnter();
    }
    
    void OnTriggerExit(Collider other)
    {
        other.GetComponent<TriggerArea>().OnExit();
    }
    
    void OnTriggerStay(Collider other)
    {
        other.GetComponent<TriggerArea>().OnStay();
    }
    #endregion
    
}
