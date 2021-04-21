using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombineCanvas : MonoBehaviour
{
    public GameObject combinePanel;
    public SO_TrackerGamePhase gamePhase_SO;
    public GameObject buttonPf;
    public Transform contentParent;

    private TowerObject towerObj;
    private List<GameObject> buttonList;
    private void Awake()
    {
        combinePanel.SetActive(false);
        buttonPf.SetActive(false);
        buttonList = new List<GameObject>();
    }

    public void ShowCombination(TowerObject twrObj)
    {
        if (twrObj == null)
        {
            combinePanel.SetActive(false);
            return;
        }
        
        
        towerObj = twrObj;
        //Check if tower has combination. If yes turn it on and update UI

        //Reset button
        if (buttonList.Count != 0)
        {
            foreach (var button in buttonList)
            {
                Destroy(button);
            }
            buttonList.Clear();
        }
        //Temp combinable tower list
        if (towerObj.combinableTowerList.Count != 0)
        {
            combinePanel.SetActive(true);
            foreach(var tower in towerObj.combinableTowerList)
            {
                GameObject button = Instantiate(buttonPf, contentParent);
                button.SetActive(true);
                button.GetComponent<CombineButton>().SetButtonText(this, tower);
                buttonList.Add(button);
            }
        }
        
    }
    public void UpgradeTower(string combinableTwr)
    {
        Debug.Log("Upgrade tower : " + towerObj.towerName + " to : " + combinableTwr);
    }
}
