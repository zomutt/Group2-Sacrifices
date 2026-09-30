using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    private TextMeshPro textMesh;
    
    private Animator anim;
    
    private readonly float timeBeforeDisable = 1.5f;

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
        textMesh.text = "-" + damage;
        
        Billboard();
        transform.position += -transform.forward;
    }
    
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
