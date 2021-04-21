using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class LeftStatsPanel : MonoBehaviour
{
    public Transform leftStatsPanel;
    public TextMeshProUGUI towerName;
    public Image atkTypeIcon;
    public TextMeshProUGUI towerDescription;
    public TextMeshProUGUI atkDmg;
    public TextMeshProUGUI atkSpd;
    public TextMeshProUGUI killCount;

    public Sprite[] atkTypeIconSelection;
    private TowerObject boundTowerObj;

    private void Awake()
    {
        leftStatsPanel.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        if (boundTowerObj != null)
            boundTowerObj.onStatChange -= towerStatChange;
    }
    public bool Bind(TowerObject towerObj)
    {
        //Break if towerinfo is null or same as bounded 
        if (boundTowerObj == towerObj)
        {
            if (boundTowerObj == null)
            {
                leftStatsPanel.gameObject.SetActive(false);
                Debug.Log("Set panel to false;");
                return false;
            }
            
        }

        //Unsubs from old stats

        if (boundTowerObj != null)
            boundTowerObj.onStatChange -= towerStatChange;

        boundTowerObj = towerObj; //Set bound

        //Clear old text

        //Initialize new stat
        if (boundTowerObj != null)
        {
            boundTowerObj.onStatChange += towerStatChange;
            leftStatsPanel.gameObject.SetActive(true);
            UpdateTwrSelectionUI();
            return true;
        }
        else //If its null
        {
            leftStatsPanel.gameObject.SetActive(false);
            return false;
        }
    }

    private void UpdateTwrSelectionUI()
    {
        //Get land air Icon
        Sprite tempLandAirIcon = atkTypeIconSelection[3];
        //Get Land Air Description
        string landAirDescription = "";
        switch (boundTowerObj.towerInfo.atkType)
        {
            case Tower.attackType.Land:
                landAirDescription = "Attacks Land Units Only";
                tempLandAirIcon = atkTypeIconSelection[0];
                break;
            case Tower.attackType.Air:
                landAirDescription = "Attacks Air Units Only";
                tempLandAirIcon = atkTypeIconSelection[2];
                break;
            case Tower.attackType.Both:
                landAirDescription = "Attacks Land and Air Units";
                tempLandAirIcon = atkTypeIconSelection[1];
                break;
        }

        towerName.text = boundTowerObj.towerName;
        atkTypeIcon.sprite = tempLandAirIcon;  
        towerDescription.text =
            landAirDescription +
            "\n\n" +
            "Soon to be added description";
        atkDmg.text = boundTowerObj.towerInfo.atkDamage.ToString();
        atkSpd.text = boundTowerObj.towerInfo.atkSpeed.ToString();
        killCount.text = boundTowerObj.killCount.ToString();
    }
    private void towerStatChange(TowerObject obj)
    {
        UpdateTwrSelectionUI();
    }
}
