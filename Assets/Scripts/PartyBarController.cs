using UnityEngine;

public class PartyBarController : MonoBehaviour
{
    private MeterDisplay meter;

    [SerializeField]
    private AudioClip clickSound;

    private void Awake()
    {
        meter = GetComponent<MeterDisplay>();
    }

    void Start()
    {
        meter.SetMeterValueAndMax(0, 4);
    }

    void Update()
    {
        meter.SetMeterValue(CombatManager.Instance.PlayerTurnTimer);
    }

    public void SkipTurnsButton(int toTurn)
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);

        CombatManager.Instance.SkipToTurn(toTurn);
    }
}
