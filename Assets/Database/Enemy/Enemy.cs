using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy 
{
    public string enemyName;
    public float maxHP;
    public float speed;
    public float armor;
    public WalkMethod walkMethod;

    //Enum
    public enum WalkMethod { air, land }

}
