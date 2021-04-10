using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Event_UI : MonoBehaviour
{
    public static Action onUpdateUI;

    public static void Trigger_UpdateUI()
    {
        onUpdateUI?.Invoke();
    }
}
