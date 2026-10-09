using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;

    void Update()
    {
        SetCrosshairPosition();
    }

    public void OnClickTarget(int index)
    {
        CombatManager.Instance.TrySetTarget(index);
    }
    
    private void SetCrosshairPosition()
    {
        int index = CombatManager.Instance.pinnedIndex % 4;
        bool party = CombatManager.Instance.pinnedIndex < 4;
        
        crosshair.localPosition =
            new Vector3((party ? -1 : 1) * (100 + index * 75f), -75);
    }
}
