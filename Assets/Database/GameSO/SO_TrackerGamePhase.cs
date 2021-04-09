using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Tracker_GamePhase", menuName = "ScriptableObject/Tracker_GamePhase")]
public class SO_TrackerGamePhase : ScriptableObject
{
    public SO_Resource resourceDB;
    public GamePhase gamePhase;

    public static event Action PhaseGameLoaded;
    public static event Action PhaseBuildPhase;
    public static event Action PhaseSelectGem;
    public static event Action PhaseFightWave;
    public static event Action PhaseGameOver;

    public void ResetGamePhase()
    {
        gamePhase = GamePhase.GameLoading;
    }
    public void GameOver()
    {
        gamePhase = GamePhase.GameOver;
        PhaseGameOver?.Invoke();
    }
    public void StartNextPhase()
    {
        //Switch to next phase and invoke the event
        switch (gamePhase)
        {
            case GamePhase.GameLoading:
                gamePhase = GamePhase.GameLoaded;
                PhaseGameLoaded?.Invoke();
                break;
            case GamePhase.GameLoaded:
                gamePhase = GamePhase.BuildPhase;
                PhaseBuildPhase?.Invoke();
                break;
            case GamePhase.BuildPhase:
                gamePhase = GamePhase.SelectGem;
                PhaseSelectGem?.Invoke();
                break;
            case GamePhase.SelectGem:
                gamePhase = GamePhase.FightWave;
                PhaseFightWave?.Invoke();
                break;
            case GamePhase.FightWave:
                gamePhase = GamePhase.BuildPhase;
                PhaseBuildPhase?.Invoke();
                break;
        }
    }
   
}
