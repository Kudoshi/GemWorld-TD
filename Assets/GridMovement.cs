using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    protected Plane Plane;
    public CustomGrid customGrid;

    [Header("Move")]
    public bool Move;
    public float moveSpacing;
    public float moveThreshold;
    public bool moveAfterSwipeEnd;
    public GameObject cam;
    public float camSmoothSpeed = 10f;

    private Vector3 camOffset;
 
    private Vector2 startTouchPos;
    private LayerMask layerMask;
    private void Start()
    {
        camOffset = cam.transform.position - transform.position;
        layerMask = LayerMask.GetMask("Floor");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Move)
        {
            Touch currentTouch = Input.GetTouch(0);
            Vector2 currentTouchPos;

            //TOUCH PHASE [BEGAN]
            //Register first and current touch
            if (currentTouch.phase == TouchPhase.Began)
            {
                startTouchPos = currentTouch.position;
            }

            //TOUCH PHASE [MOVED]

            if (currentTouch.phase == TouchPhase.Moved)
            {
                if (!moveAfterSwipeEnd)
                {
                    currentTouchPos = currentTouch.position;
                    checkSwipe(currentTouchPos);
                }

            }

            if (currentTouch.phase == TouchPhase.Ended)
            {
                currentTouchPos = currentTouch.position;
                checkSwipe(currentTouchPos);
            }
        }
    }
    private void checkSwipe(Vector2 currentTouchPos)
    {
        //Register Swipe Value
        float verticalSwipeVal = MobileInputExtension.verticalSwipeVal(startTouchPos, currentTouchPos);
        float horizontalSwipeVal = MobileInputExtension.horizontalSwipeVal(startTouchPos, currentTouchPos);
        //Check if Vertical swipe
        if (verticalSwipeVal > moveThreshold && verticalSwipeVal > horizontalSwipeVal)
        {
            //Cache GameObject transform
            Transform transform = gameObject.transform;
            //Up
            if (currentTouchPos.y - startTouchPos.y > 0)
            {
                Vector3 moveValue = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpacing);
                GridMoveGameObject(moveValue);

            }
            //Down
            else if (currentTouchPos.y - startTouchPos.y < 0)
            {
                Vector3 moveValue = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpacing);
                GridMoveGameObject(moveValue);

            }

            //Re-registering startTouchPos
            startTouchPos = currentTouchPos;
        }

        //Check if Horizontal swipe
        else if (horizontalSwipeVal > moveThreshold && horizontalSwipeVal > verticalSwipeVal)
        {
            //Right
            if (currentTouchPos.x - startTouchPos.x > 0)
            {
                Vector3 moveValue = new Vector3(transform.position.x + moveSpacing, transform.position.y, transform.position.z);
                GridMoveGameObject(moveValue);

            }
            //Left
            else if (currentTouchPos.x - startTouchPos.x < 0)
            {
                Vector3 moveValue = new Vector3(transform.position.x - moveSpacing, transform.position.y, transform.position.z);
                GridMoveGameObject(moveValue);
            }
            startTouchPos = currentTouchPos;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    private void GridMoveGameObject(Vector3 moveValue)
    {
        //Debug.Log("GridMove");
        Vector3 initialPos = transform.position;
        Vector3 finalPos = customGrid.GetNearestPointOnGrid(moveValue);
        gameObject.transform.position = finalPos;
    }

    private void LateUpdate()
    {
        //Cast ray to floor point 
        RaycastHit raycastInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastInfo, 100f, layerMask))
        {
            //Move camera if camera is not centered
            if (raycastInfo.point.x != transform.position.x || raycastInfo.point.z != transform.position.z)
            {
                Vector3 distancePointObj =  transform.position - raycastInfo.point;
                Vector3 moveValue = new Vector3(distancePointObj.x, 0, distancePointObj.z);
                Vector3 newPos = cam.transform.position + moveValue;
                Vector3 smoothedCamPos = Vector3.Lerp(cam.transform.position, newPos, camSmoothSpeed * Time.deltaTime);
                cam.transform.position = smoothedCamPos;
            }
            
        }
    }
}
