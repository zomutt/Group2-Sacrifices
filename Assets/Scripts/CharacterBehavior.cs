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

    void Update()
    {
        ModelOrientation();
    }
    
    private void ModelOrientation()
    {
        if (PartyController.Instance.enabled)
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
}
