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
    public int magic;

    [Space(10f)]
    public int armor;

    public bool HasHealthRemaining => health > 0;
    public float HealthPercent => (float)health / maxHealth;

    public void RefillHealth()
    {
        health = maxHealth;
    }
}
