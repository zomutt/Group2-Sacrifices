using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform partyTf;
    
    [SerializeField]
    private float moveSpeed;
    
    private Animator anim;
    
    public static CameraController Instance;
    
    void Awake()
    {
        Instance = this;
        
        anim = GetComponent<Animator>();
    }
    
    void Start()
    {
        partyTf = PartyController.Instance.transform;
    }
    
    void Update()
    {
        switch (LevelManager.LevelGameState)
        {
            case GameState.Exploring:
                transform.position = Vector3.Lerp(transform.position, partyTf.position, moveSpeed * Time.deltaTime);
                anim.SetBool("Battle", false);
                break;
                
            case GameState.Battle:
                transform.position = CombatManager.Instance.transform.position;
                anim.SetBool("Battle", true);
                break;
                
            default:
                break;
        }
    }
}
