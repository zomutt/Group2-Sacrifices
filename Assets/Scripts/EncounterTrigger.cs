using UnityEngine;

public class EncounterTrigger : TriggerArea
{
    [SerializeField]
    private bool hasForcedEncounter;
    [SerializeField, Tooltip("Adding a forced encounter negates plain encounters from spawning")]
    private EncounterInfo forcedEncounter;
    
    private bool alreadyTriggered;
    
    #region Trigger
    public override void OnEnter()
    {
        if (hasForcedEncounter && !alreadyTriggered)
        {
            LevelManager.Instance.TriggerEncounter(forcedEncounter);
            alreadyTriggered = true;
        }
    }

    public override void OnExit() {}

    public override void OnStay()
    {
        if (!hasForcedEncounter)
            LevelManager.Instance.ProgressEncounterTimer(Time.fixedDeltaTime);
    }
    #endregion
}
