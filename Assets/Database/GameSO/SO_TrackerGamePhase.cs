using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Tracker_GamePhase", menuName = "ScriptableObject/Tracker_GamePhase")]
public class SO_TrackerGamePhase : ScriptableObject
{
    public GamePhase gamePhase { get; private set; }


    public static event Action onPhaseBuild;
    public static event Action onPhaseSelectGem;
    public static event Action onPhaseFightWave;
    public static event Action onPhaseGameOver;

    public void ResetGamePhase()
    {
        gamePhase = GamePhase.GameLoaded;
    }public void GameOver()
    {
        gamePhase = GamePhase.GameOver;
        onPhaseGameOver?.Invoke();
    }
    public void StartNextPhase()
    {
        
        //Switch to next phase and invoke the event
        switch (gamePhase)
        {
            case GamePhase.GameLoaded:
                gamePhase = GamePhase.BuildPhase;
                onPhaseBuild?.Invoke();
                break;
            case GamePhase.BuildPhase:
                gamePhase = GamePhase.SelectGem;
                onPhaseSelectGem?.Invoke();
                break;
            case GamePhase.SelectGem:
                gamePhase = GamePhase.FightWave;
                onPhaseFightWave?.Invoke();
                break;
            case GamePhase.FightWave:
                gamePhase = GamePhase.BuildPhase;
                onPhaseBuild?.Invoke();
                break;
        }
        Debug.Log("Change Phase to: " + gamePhase);
        Event_Text.Display_MiddleLargeText(gamePhase.ToString());
    }
   
}
