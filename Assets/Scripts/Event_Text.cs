using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Event_Text : MonoBehaviour
{
    public static Action<string> onMiddleLargeText_Display;

    public static void Display_MiddleLargeText(string text)
    {
        onMiddleLargeText_Display?.Invoke(text);
    }

    
}
