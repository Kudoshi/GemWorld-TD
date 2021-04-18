using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PressableUI : MonoBehaviour
{
    public Button OkayButton;
    private void OnEnable()
    {
        BuildManager.onCanStillBuild += CanStillBuild;
    }
    private void OnDisable()
    {
        BuildManager.onCanStillBuild -= CanStillBuild;
    }

    private void CanStillBuild()
    {
        OkayButton.interactable = true;
    }
}
