using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Event_TextListener : MonoBehaviour
{
    public bool MiddleTextDisplay = false;
    public float textTime;
    private TextMeshProUGUI textDisplay;
    private void Awake()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        textDisplay.enabled = false;
    }
    private void OnEnable()
    {
        if (MiddleTextDisplay)
            Event_Text.onMiddleLargeText_Display += OnMiddleLargeText_Display;
    }
    private void OnDisable()
    {
        if (MiddleTextDisplay)
            Event_Text.onMiddleLargeText_Display -= OnMiddleLargeText_Display;
    }

    private void OnMiddleLargeText_Display(string text)
    {
        textDisplay.enabled = true;
        textDisplay.text = text;
        Invoke("ResetTextDisplay", textTime);
    }

    private void ResetTextDisplay()
    {
        textDisplay.text = "";
        textDisplay.enabled = false;
    }
}
