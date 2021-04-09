using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tracker_GamePhaseManager : MonoBehaviour
{
    /// <summary>
    /// Resets GamePhase of SO
    /// </summary>
    public SO_TrackerGamePhase gamePhaseTracker;
    private void Awake()
    {
        //ResetGamePhase 
        gamePhaseTracker.ResetGamePhase();
        Invoke("TriggerNextGamePhase", 5);
    }
    private void TriggerNextGamePhase()
    {
        gamePhaseTracker.StartNextPhase();
    }
}
