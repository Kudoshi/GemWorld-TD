using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_UIButtonEvent : MonoBehaviour
{
    public void Trigger_onBuildButtonClick()
    {
        Event_UIButton.Trigger_BuildButtonClick();
    }
    public void Trigger_onOkayBuildButtonClick()
    {
        Event_UIButton.Trigger_OkayBuildButtonClick();
    }

}
