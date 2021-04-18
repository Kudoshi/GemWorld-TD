using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject ghostTwrPrefab;
    public GameObject randomTwrPrefab;
    public GameObject cam;
    public SO_Resource resourceSO;
    public SO_TrackerGamePhase gamePhaseSO;
    public GridMovement gridMovement;
    public static event Action onCanStillBuild;

    private GameObject towerGhost;
    private int layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Floor");
        gridMovement = GetComponent<GridMovement>();
    }
    private void OnEnable()
    {
        Event_UIButton.onBuildButtonClick += BuildButtonClick;
        Event_UIButton.onOkayBuildButtonClick += OkayBuildButtonClick;
        TowerObject.onFinishBuilding += FinishBuilding;
    }

    private void OnDisable()
    {
        Event_UIButton.onBuildButtonClick -= BuildButtonClick;
        Event_UIButton.onOkayBuildButtonClick -= OkayBuildButtonClick;
        TowerObject.onFinishBuilding -= FinishBuilding;

    }
    private void FinishBuilding(string arg1, GameObject arg2)
    {
        if (resourceSO.buildGem == 0)
        {
            if (towerGhost != null)
            {
                Destroy(towerGhost);
            }
            gamePhaseSO.StartNextPhase();
        }
        else
            onCanStillBuild?.Invoke();
    }


    private void OkayBuildButtonClick()
    {
        bool canBuild = resourceSO.CheckCanBuild();

        //Build
        if (canBuild)
        {
            Instantiate(randomTwrPrefab, towerGhost.transform.position, randomTwrPrefab.transform.rotation);
            Event_UI.Trigger_UpdateUI();
        }
        
    }
    private void BuildButtonClick()
    {
        //Spawn prefab based on camera front
        RaycastHit raycastInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastInfo, 100f, layerMask))
        {
            //Spawn ghost
            Vector3 spawnPoint = new Vector3(raycastInfo.point.x, 1, raycastInfo.point.z);
            towerGhost = Instantiate(ghostTwrPrefab, spawnPoint, ghostTwrPrefab.transform.rotation, transform);

            //Inject value to GridMovement Script
            GridMovement ghostGridMovement = towerGhost.AddComponent<GridMovement>();
            ghostGridMovement.customGrid = gridMovement.customGrid;
            ghostGridMovement.Move = true;
            ghostGridMovement.moveAfterSwipeEnd = false;
            ghostGridMovement.moveSpacing = gridMovement.moveSpacing;
            ghostGridMovement.moveThreshold = gridMovement.moveThreshold;
            ghostGridMovement.cam = gridMovement.cam;
        }
    }
    
    private void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000f, Color.red);
    }
}
