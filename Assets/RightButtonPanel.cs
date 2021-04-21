using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButtonPanel : MonoBehaviour
{
    public Transform rightButtonPanel;
    public SO_TrackerGamePhase gamePhase_SO;
    public GameObject upgradeButton;
    public GameObject downgradeButton;

    private TowerObject towerObj;
    private void Awake()
    {
        rightButtonPanel.gameObject.SetActive(false);
        upgradeButton.SetActive(false);
        downgradeButton.SetActive(false);
    }
    // Update is called once per frame
    public void ShowButtons(TowerObject obj)
    {
        if (obj == null)
        {
            rightButtonPanel.gameObject.SetActive(false);
            return;
        }
        towerObj = obj;
        if (gamePhase_SO.gamePhase == GamePhase.SelectGem)
        {
            //Reset panel
            rightButtonPanel.gameObject.SetActive(true);
            upgradeButton.SetActive(false);
            downgradeButton.SetActive(false);

            //Activate
            if (obj.upgradableTowerList.Count != 0)
                upgradeButton.SetActive(true);
            if (obj.canDowngrade)
                downgradeButton.SetActive(true);
        }
        else
            rightButtonPanel.gameObject.SetActive(false);

    }
}
