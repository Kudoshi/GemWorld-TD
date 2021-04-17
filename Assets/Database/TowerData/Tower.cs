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
    public attackType atkType;
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
    public enum Type
    {
        Amethyst, Aquamarine, Diamond, Emerald, Opal, Ruby, Sapphire, Topaz
    }
    public enum attackType
    {
        Both, Land, Air, 
    }
}
