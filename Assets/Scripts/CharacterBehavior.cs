using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    
    
    public void Sacrifice()
    {
        // Remove from party controller
        
        // Remove from party holder and hide
        transform.parent = transform.parent.parent;
        gameObject.SetActive(false);
    }
}
