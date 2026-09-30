using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    [SerializeField]
    protected int power;

    public abstract void MoveOnTarget(CharacterBehavior from, CharacterBehavior to);
}
