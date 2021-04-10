using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Event_UIButton : MonoBehaviour
{
    public static Action onBuildButtonClick;

    public static void Trigger_BuildButtonClick()
    {
        onBuildButtonClick?.Invoke();
    }

    public static Action onOkayBuildButtonClick;

    public static void Trigger_OkayBuildButtonClick()
    {
        onOkayBuildButtonClick?.Invoke();
    }
}
