using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data_GameDifficulty", menuName = "ScriptableObject/Data_GameDifficulty")]
public class SO_GameDifficulty : ScriptableObject
{
   /// <summary>
   /// Not implemented yet
   /// Setting game difficutly when coming in from the main menu
   /// </summary>
    public enum GameDifficulty
    {
        Easy, Medium, Hard, Fairy
    }
    public GameDifficulty gameDifficulty;
    
}
