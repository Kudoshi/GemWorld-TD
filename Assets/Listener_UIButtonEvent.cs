using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Listener_UIButtonEvent : MonoBehaviour
{
    public UnityEvent onPhaseBuild;
    public UnityEvent onBuildButtonClick;
    public UnityEvent onPhaseSelectGem;
    
    private void OnEnable()
    {
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;
        Event_UIButton.onBuildButtonClick += BuildButtonClick;
        SO_TrackerGamePhase.onPhaseSelectGem += PhaseSelectGem;
    }

    private void OnDisable()
    {
        Event_UIButton.onBuildButtonClick -= BuildButtonClick;
        SO_TrackerGamePhase.onPhaseBuild -= PhaseBuild;
        SO_TrackerGamePhase.onPhaseSelectGem -= PhaseSelectGem;

    }

    private void PhaseBuild()
    {
        onPhaseBuild?.Invoke();
    }
    private void PhaseSelectGem()
    {
        onPhaseSelectGem?.Invoke();
    }
    private void BuildButtonClick()
    {
        onBuildButtonClick?.Invoke();
    }
}
