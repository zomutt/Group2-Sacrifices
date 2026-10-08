using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    private TextMeshPro textMesh;
    
    private Animator anim;
    
    private readonly float timeBeforeDisable = 1.5f;
    
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private Color healColor;

    void Awake()
    {
        anim = GetComponent<Animator>();
        
        textMesh = transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        anim.SetTrigger("Ping");
        
        Invoke("DisableSelf", timeBeforeDisable);
    }

    void Update()
    {
        Billboard();
    }
    
    private void Billboard()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetDamageNumber(int damage)
    {
        if (damage > 0)
        {
            textMesh.text = "+" + damage;
            textMesh.color = healColor;
        }
        else
        {
            textMesh.text = "" + damage;
            textMesh.color = damageColor;
        }
        
        Billboard();
        transform.position += -transform.forward;
    }
    
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
