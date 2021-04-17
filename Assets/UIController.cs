using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public LeftStatsPanel leftStatsCanvas;
    private void OnEnable()
    {
        UITowerSelect.onSelectedTowerChanged += SelectedTowerChanged;
        SelectedTowerChanged(UITowerSelect.SelectedTower);
    }
    private void OnDisable()
    {
        UITowerSelect.onSelectedTowerChanged -= SelectedTowerChanged;
    }
    private void SelectedTowerChanged(TowerObject obj)
    {
        leftStatsCanvas.Bind(obj);
    }

    
}
