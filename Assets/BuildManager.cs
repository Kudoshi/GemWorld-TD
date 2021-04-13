using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject ghostTwrPrefab;
    public GameObject randomTwrPrefab;
    public SO_Resource resourceSO;
    public SO_TrackerGamePhase gamePhaseSO;
    public GameObject cam;
    public GridMovement gridMovement;

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
    }

   

    private void OnDisable()
    {
        Event_UIButton.onBuildButtonClick -= BuildButtonClick;
        Event_UIButton.onOkayBuildButtonClick -= OkayBuildButtonClick;

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
        
        //Check if gem is empty - Start next phase

        if (resourceSO.buildGem == 0)
        {
            if (towerGhost != null)
            {
                Destroy(towerGhost);
            }
            else
                Debug.LogWarning("ERROR: tower Ghost not found");

            gamePhaseSO.StartNextPhase();
        }
        
    }
    private void BuildButtonClick()
    {
        //Spawn prefab based on camera front
        RaycastHit raycastInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastInfo, 100f, layerMask))
        {
            //Spawn ghost
            //Vector3 spawnPoint = raycastInfo.point;
            Vector3 spawnPoint = new Vector3(raycastInfo.point.x, 1, raycastInfo.point.z);
            towerGhost = Instantiate(ghostTwrPrefab, spawnPoint, ghostTwrPrefab.transform.rotation, transform);
            //Inject value to ghost movement
            GridMovement ghostGridMovement = towerGhost.AddComponent<GridMovement>();
            ghostGridMovement.customGrid = gridMovement.customGrid;
            ghostGridMovement.Move = true;
            ghostGridMovement.moveAfterSwipeEnd = false;
            ghostGridMovement.moveSpacing = gridMovement.moveSpacing;
            ghostGridMovement.moveThreshold = gridMovement.moveThreshold;
            ghostGridMovement.cam = gridMovement.cam;
        }

        //Attach Grid movement to it
    }
    
    private void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000f, Color.red);

    }
}
