using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "ScriptableObjects/Attacks/MultiSpawn")]
public class MultiSpawn : SummonMove
{
    [SerializeField]
    private int repeatCount = 2;
    
    public override void MoveOnTarget(CharacterBehavior from, CharacterBehavior to)
    {
        for (int i = 0; i < 2; i++)
        {
            base.MoveOnTarget(from, to);
        }
    }
}
