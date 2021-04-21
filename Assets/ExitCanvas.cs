using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCanvas : MonoBehaviour
{
    public GameObject exitButton;
    public UIController uiController;
    private void Awake()
    {
        exitButton.SetActive(false);
    }
    public void UIExitButtonActivate()
    {
        if (UIController.uiOpen)
        {
            exitButton.SetActive(true);
        }
        else
        {
            exitButton.SetActive(false);
        }
    }
    public void onExitButtonClicked()
    {
        uiController.SelectedTowerChanged(null, null);
    }
}
