using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAbleToBuildChecker : MonoBehaviour
{
    public Material canBuildMat;
    public Material noBuildMat;
    public AbleToBuildChecker ableToBuildCheckerManager;

    public bool buildable = true;
    private Renderer meshRenderer;

    

    [Header("Info")]
    public string collidedWith;
    public bool viewBuildable;
    public List<GameObject> bindedObj;
    private void Awake()
    {
        bindedObj = new List<GameObject>();
        meshRenderer = GetComponent<Renderer>();
        meshRenderer.material = noBuildMat;
    }

    

    private void Update()
    {
        viewBuildable = buildable;

        if (bindedObj.Count == 0) //Can build
        {
            collidedWith = "Nothing";
            meshRenderer.material = canBuildMat;
            ableToBuildCheckerManager.RegisterBuildable(gameObject, true);
        }
        else
        {
            //Console.WriteLine(String.Join(", ", numbersStrLst));//Output:"One, Two, Three, Four, Five"

            collidedWith = string.Join(", ", bindedObj);
            meshRenderer.material = noBuildMat;
            ableToBuildCheckerManager.RegisterBuildable(gameObject, false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("TravelPoint"))
        {
            if (!bindedObj.Contains(other.gameObject))
            {
                bindedObj.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("TravelPoint"))
        {
            //Leave it blank for now
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("TravelPoint"))
        {
            if (bindedObj.Contains(other.gameObject))
            {
                bindedObj.Remove(other.gameObject);
            }
            else
                Debug.Log("Something wrong happened. Detected foreign object that are not registered");

        }
    }
}
