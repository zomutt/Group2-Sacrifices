using UnityEngine;
using System;

[Serializable]
public class CharacterStats
{
    [HideInInspector]
    public int health;
    public int maxHealth;

    [Space(10f)]
    public int strength;
    public int luck;

    [Space(10f)]
    public int armor;

    public bool HasHealthRemaining
    {
        get { return health > 0; }
    }

    public void RefillHealth()
    {
        health = maxHealth;
    }
}
