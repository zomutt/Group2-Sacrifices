using UnityEngine;

public class PartyBarController : MonoBehaviour
{
    private MeterDisplay meter;

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
}
