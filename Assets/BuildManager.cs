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
    public CustomGrid grid;
    public static event Action<bool> onCanStillBuild;

    private GameObject towerGhost;
    private int layerMask;
    private AbleToBuildChecker buildChecker;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Floor");
        gridMovement = GetComponent<GridMovement>();
    }
    private void OnEnable()
    {
        Event_UIButton.onOkayBuildButtonClick += OkayBuildButtonClick;
        TowerObject.onFinishBuilding += FinishBuilding;
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;
    }

  
    private void OnDisable()
    {
        Event_UIButton.onOkayBuildButtonClick -= OkayBuildButtonClick;
        TowerObject.onFinishBuilding -= FinishBuilding;
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;

    }
    private void FinishBuilding(string arg1, GameObject arg2)
    {
        if (!resourceSO.CheckCanBuild() & gamePhaseSO.gamePhase == GamePhase.BuildPhase)
        {
            if (towerGhost != null)
            {
                Destroy(towerGhost);
            }
            gamePhaseSO.StartNextPhase();
        }
    }


    private void OkayBuildButtonClick()
    {
        bool canBuild = resourceSO.CheckCanBuild();
        bool checkBuildable = buildChecker.GetIfCanBuild();
        //Build
        if (canBuild & checkBuildable)
        {
            resourceSO.ModifyStats(SO_Resource.Stats.buildGem, -1);
            Instantiate(randomTwrPrefab, towerGhost.transform.position, randomTwrPrefab.transform.rotation);
            Event_UI.Trigger_UpdateUI();
            
        }
        else if (!checkBuildable)
        {
            MessageManager.InvokeDisplayMessage(MessageManager.DisplayLocation.Top_Middle, "Unable to build there", 3.0f);
        }
        onCanStillBuild?.Invoke(resourceSO.CheckCanBuild());
        
    }
    private void PhaseBuild()
    {
        //Spawn prefab based on camera front
        RaycastHit raycastInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastInfo, 100f, layerMask))
        {
            //Spawn ghost
            Vector3 spawnPoint = new Vector3(raycastInfo.point.x, 1, raycastInfo.point.z);
            spawnPoint = grid.GetNearestPointOnGrid(spawnPoint);
            towerGhost = Instantiate(ghostTwrPrefab, spawnPoint, ghostTwrPrefab.transform.rotation, transform);

            //Inject value to GridMovement Script
            GridMovement ghostGridMovement = towerGhost.AddComponent<GridMovement>();
            ghostGridMovement.customGrid = gridMovement.customGrid;
            ghostGridMovement.Move = true;
            ghostGridMovement.moveAfterSwipeEnd = false;
            ghostGridMovement.moveSpacing = gridMovement.moveSpacing;
            ghostGridMovement.moveThreshold = gridMovement.moveThreshold;
            ghostGridMovement.cam = gridMovement.cam;

            //Hook to build checker
            buildChecker = towerGhost.GetComponent<AbleToBuildChecker>();
        }
    }
    
    private void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000f, Color.red);
    }
}
