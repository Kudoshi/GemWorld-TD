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
        gamePhaseTracker.ResetGamePhase();
        Invoke("TriggerNextGamePhase", 1.5f);
    }
    public void TriggerNextGamePhase()
    {
        gamePhaseTracker.StartNextPhase();

    }
}
