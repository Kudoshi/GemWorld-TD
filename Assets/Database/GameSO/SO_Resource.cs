using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tracker_Resource", menuName = "ScriptableObject/Tracker_Resource")]
public class SO_Resource : ScriptableObject
{
    private int KingdomMaxHealth =100;
    private int MaxBuildGem = 5;
    public int KingdomHealth;
    public int BuildGem;
    public int gemChanceLevel;
    public int Gold;

    public void ResetResource()
    {
        KingdomHealth = KingdomMaxHealth;
        BuildGem = 0;
        gemChanceLevel = 1;
        Gold = 1;
    }
}
