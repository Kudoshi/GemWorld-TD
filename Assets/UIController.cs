using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public SO_TrackerGamePhase gamePhaseSO;
    public LeftStatsPanel leftStatsCanvas;
    public RightButtonPanel rightButtonPanel;
    public CombineCanvas combineCanvas;
    public ExitCanvas exitCanvas;

    public static bool uiOpen;

    private void OnEnable()
    {
        UITowerSelect.onSelectedTowerChanged += SelectedTowerChanged;
    }
    private void OnDisable()
    {
        UITowerSelect.onSelectedTowerChanged -= SelectedTowerChanged;
    }
    public void SelectedTowerChanged(TowerObject obj, GameObject GO)
    {
        //For tower select period
        switch (gamePhaseSO.gamePhase)
        {
            case GamePhase.GameLoaded:
                break;
            case GamePhase.BuildPhase:
                break;
            case GamePhase.SelectGem:
                uiOpen = leftStatsCanvas.Bind(obj);
                rightButtonPanel.ShowButtons(obj);
                combineCanvas.ShowCombination(obj);
                exitCanvas.UIExitButtonActivate();
                Debug.Log(uiOpen + " - twrObj " + obj);

                break;
            case GamePhase.FightWave:
                break;
            case GamePhase.GameOver:
                break;
            default:
                break;
        }
    }

    
}
