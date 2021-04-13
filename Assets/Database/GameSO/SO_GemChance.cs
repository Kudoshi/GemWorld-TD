using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_GemChance", menuName = "ScriptableObject/Data_GemChance")]
public class SO_GemChance : ScriptableObject
{
    public TierChance[] GemTierChanceList;
}
