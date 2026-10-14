using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;
    private Animator crosshairAnim;
    
    private int lastPinnedIndex;

    void Awake()
    {
        crosshairAnim = crosshair.GetComponent<Animator>();
    }

    void OnEnable()
    {
        SetCrosshairPosition(CombatManager.Instance.pinnedIndex);
    }

    void Update()
    {
        if (CombatManager.Instance.pinnedIndex != lastPinnedIndex)
            SetCrosshairPosition(CombatManager.Instance.pinnedIndex);
    }

    public void OnClickTarget(int index)
    {
        CombatManager.Instance.TrySetTarget(index);
    }
    
    private void SetCrosshairPosition(int pinnedIndex)
    {
        lastPinnedIndex = pinnedIndex;
        
        int index = pinnedIndex % 4;
        bool party = pinnedIndex < 4;
        
        crosshair.localPosition = new Vector3((party ? -1 : 1) * (100 + index * 75f), -75);
        crosshairAnim.SetTrigger("Place");
    }
}
