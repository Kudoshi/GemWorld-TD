using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
/// <summary>
/// Not done: Right panel UI, ray cast, disabling this script when on build phase
/// </summary>
public class UITowerSelect : MonoBehaviour
{
    public Camera cam;
    public float tapTimeThreshold; // The shorter it is the shorter the tap
    public float tapMoveThreshold;
    public Vector3 camOffsetOnTower;
    public static event Action<TowerObject, GameObject> onSelectedTowerChanged;
    public static TowerObject SelectedTower { get; private set; }

    private Vector2 startEndTime;
    private Vector2 startTapPos;
    private Vector2 endTapPos;
    private void Awake()
    {
        SceneManager.LoadSceneAsync("UI_Tower", LoadSceneMode.Additive);
    }
    void Update()
    {
        RegisterTap();
    }
    private void RegisterTap()
    {
        if (Input.touchCount == 1)
        {
            var finger1 = Input.GetTouch(0);
            if (finger1.phase == TouchPhase.Began)
            {
                //onSelectedTowerChanged?.Invoke(null);
                //Register Var
                startTapPos = finger1.position;
                startEndTime.x = Time.time;
            }
            if (finger1.phase == TouchPhase.Ended)
            {
                //Register var
                endTapPos = finger1.position;
                startEndTime.y = Time.time;

                //Calculation
                float timeInBetween = startEndTime.y - startEndTime.x;
                float distanceInBetween = Vector3.Distance(startTapPos, endTapPos);

                if (timeInBetween <= tapTimeThreshold && distanceInBetween <= tapMoveThreshold)
                {   
                    SelectTower();
                    
                }
            }
        }
    }

    private void CameraRecenterTower(RaycastHit hitInfo)
    {
        cam.transform.position = hitInfo.collider.transform.position + camOffsetOnTower;
    }

    private void SelectTower()
    {
        var ray = cam.ScreenPointToRay(startTapPos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.CompareTag("Rock"))
            {
                onSelectedTowerChanged?.Invoke(null, hitInfo.collider.gameObject);
            }
            else if (hitInfo.collider.CompareTag("Tower"))
            {
                CameraRecenterTower(hitInfo);
                var tower = hitInfo.collider.GetComponentInParent<TowerObject>();

                SelectedTower = tower;
                onSelectedTowerChanged?.Invoke(tower, hitInfo.collider.gameObject);
            }
            else
            {
               // onSelectedTowerChanged?.Invoke(null, hitInfo.collider.gameObject);
            }
        }
    }
}
