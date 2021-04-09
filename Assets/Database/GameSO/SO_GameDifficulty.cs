using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data_GameDifficulty", menuName = "ScriptableObject/Data_GameDifficulty")]
public class SO_GameDifficulty : ScriptableObject
{
    public enum GameDifficulty
    {
        Easy, Medium, Hard, Fairy
    }
    public GameDifficulty gameDifficulty;
}
