using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class MessageManager : MonoBehaviour
{
    public GameObject Top_MiddleMsgPanel;
    public TextMeshProUGUI Top_MiddleMsg;
    public Coroutine Top_MiddleCoroutine;
    public static event Action<string, float> onDisplay_Top_MiddleMsg;

    private void OnEnable()
    {
        onDisplay_Top_MiddleMsg += Display_Top_MiddleMsg;
    }
    private void OnDisable()
    {
        onDisplay_Top_MiddleMsg -= Display_Top_MiddleMsg;
    }
    public enum DisplayLocation
    {
        Top_Middle,
    }
    public static void InvokeDisplayMessage(DisplayLocation displayLocation, string message, float duration)
    {
        switch (displayLocation)
        {
            case DisplayLocation.Top_Middle:
                onDisplay_Top_MiddleMsg?.Invoke(message, duration);
                break;
            default:
                break;
        }
    }

    /////////////////////////////////////////////////////////////////////
    // TOP MIDDLE MESSAGE
    private void Display_Top_MiddleMsg(string message, float duration)
    {
        if (Top_MiddleCoroutine != null)
            StopCoroutine(Top_MiddleCoroutine);
        Top_MiddleCoroutine = StartCoroutine(IEnum_Display_Top_MiddleMsg(message, duration));
    }

    IEnumerator IEnum_Display_Top_MiddleMsg(string message, float duration)
    {
        Top_MiddleMsgPanel.SetActive(true);
        Top_MiddleMsg.text = message.ToString();
        yield return new WaitForSeconds(duration);
        ClearDisplay(Top_MiddleMsgPanel, Top_MiddleMsg);
    }

    /////////////////////////////////////////////////////////////////////
    // 
    private void ClearDisplay(GameObject panel, TextMeshProUGUI panelText)
    {
        panelText.text = " ";
        panel.SetActive(false);
    }
    private void Awake()
    {
        //Clear all msg
        ClearDisplay(Top_MiddleMsgPanel, Top_MiddleMsg);
    }

    
}
