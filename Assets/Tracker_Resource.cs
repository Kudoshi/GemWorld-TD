using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tracker_Resource : MonoBehaviour
{
    public SO_Resource resourceSO;
    private void Awake()
    {
        resourceSO.ResetResource();
    }
    private void OnEnable()
    {
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;

    }
    private void OnDisable()
    {
        SO_TrackerGamePhase.onPhaseBuild -= PhaseBuild;

    }
    private void PhaseBuild() 
    {
        resourceSO.AddGemDefault();
        resourceSO.AddGold(10);
        Event_UI.Trigger_UpdateUI();
    }

    
}
