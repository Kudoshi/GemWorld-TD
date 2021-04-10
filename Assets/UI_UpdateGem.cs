using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_UpdateGem : MonoBehaviour
{
    public SO_Resource resourceSO;
    public TextMeshProUGUI gemUI;

    private void Start()
    {
        UpdateUI();
    }
    private void OnEnable()
    {
        Event_UI.onUpdateUI += UpdateUI;

    }
    private void OnDisable()
    {
        Event_UI.onUpdateUI -= UpdateUI;

    }

    private void UpdateUI()
    {
        gemUI.text = resourceSO.buildGem.ToString();
    }
}
