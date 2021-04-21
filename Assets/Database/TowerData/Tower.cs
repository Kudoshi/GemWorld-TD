using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    public static Tier GetNextTier(Tier currentTier)
    {
        int nextTierIndex = (int)currentTier + 1;
        //Check if exceed Tier Index
        if (nextTierIndex >= (Enum.GetNames(typeof(Tier)).Length - 1))
            return currentTier;

        return (Tier)nextTierIndex;
    }
    public static Tier GetNextTier(string currentTierName)
    {
        Tier currentTier = (Tier)Enum.Parse(typeof(Tier), currentTierName);
        int nextTierIndex = (int)currentTier + 1;
        //Check if exceed Tier Index
        if (nextTierIndex >= (Enum.GetNames(typeof(Tier)).Length - 1))
            return currentTier;

        return (Tier)nextTierIndex;
    }
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
