using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombineButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    private string combinableTower;
    private CombineCanvas combineScript;
    public void SetButtonText(CombineCanvas combineScript, string towerName)
    {
        this.combineScript = combineScript;
        combinableTower = towerName;
        text.text = towerName;
    }
    public void OnButtonClick()
    {
        combineScript.UpgradeTower(combinableTower);
        Debug.Log("Combine into : " + combinableTower);
    }
   
}
