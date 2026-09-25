using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyController : MonoBehaviour
{
    private InputSystem_Actions controls;
    
    private Vector2 moveDir;
    [SerializeField]
    private float moveSpeed;
    
    private Rigidbody rb;
    
    [SerializeField]
    private List<CharacterBehavior> characters;
    
    private List<Vector3> trail = new();
    private float pullAmount;
    private readonly float trailStep = 0.25f;
    
    public static PartyController Instance;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        controls = new InputSystem_Actions();
        
        Instance = this;
    }
    
    void Start()
    {
        ChangeTrailLength(Mathf.CeilToInt(characters.Count / trailStep) + 1);
    }

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

    void Update()
    {
        Move();
        
        PullTrail();
    }
    
    private void Move()
    {
        rb.linearVelocity = new Vector3(moveDir.x, 0, moveDir.y) * moveSpeed;
    }
    
    private void SetMoveDir(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
    }
    
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
        
        for (int i = 1; i < characters.Count; i++)
        {
            float temp = i / trailStep - pullAmount;
            
            int index = Mathf.CeilToInt(temp);
            float remainder = (temp % 1 + 1) % 1;
            
            characters[i].transform.position =
                trail[index]
                + VectTrailToPrevTrail(index).normalized * pullAmount;
        }
    }
    
    private Vector3 VectTrailToPrevTrail(int index)
    {
        if (index <= 0)
            return transform.position - trail[0];
        else
            return trail[index - 1] - trail[index];
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
}
