using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PressableUI : MonoBehaviour
{
    public GameObject OkayButtonPanel;
    public Button OkayButton;
    private void OnEnable()
    {
        BuildManager.onCanStillBuild += CanStillBuild;
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;
        SO_TrackerGamePhase.onPhaseSelectGem += SelectGem;
    }

    private void PhaseBuild()
    {
        OkayButtonPanel.SetActive(true);
        OkayButton.interactable = true;
    }

    private void SelectGem()
    {
        OkayButton.interactable = false;
        OkayButtonPanel.SetActive(false);
    }
    private void Awake()
    {
        OkayButtonPanel.SetActive(false);
    }
    private void OnDisable()
    {
        BuildManager.onCanStillBuild -= CanStillBuild;
        SO_TrackerGamePhase.onPhaseSelectGem -= SelectGem;
    }

    private void CanStillBuild(bool buildable)
    {
        if (buildable)
            OkayButton.interactable = true;
        else
            OkayButton.interactable = false;
    }
}
