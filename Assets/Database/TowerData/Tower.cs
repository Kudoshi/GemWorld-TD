using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tower
{
    
    public string towerName;
    public Tier tier;
    public float atkSpeed;
    public float atkRange;
    public float atkDamage;
    public float atkProjectileSpeed;

    //Enums

    public enum Tier
    {
        Chipped,
        Flawed,
        Normal,
        Flawless,
        Perfect,
        Great,
    }
}
