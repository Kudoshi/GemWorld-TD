using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAbleToBuildChecker : MonoBehaviour
{
    public Material canBuildMat;
    public Material noBuildMat;
    public AbleToBuildChecker ableToBuildCheckerManager;

    private bool buildable = true;
    private GameObject bindedObj = null;
    private Renderer meshRenderer;
    public string collidedWith;
    public bool viewBuildable;
    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        meshRenderer.material = noBuildMat;
    }

    

    private void Update()
    {
        viewBuildable = buildable;
        collidedWith = "Empty";
        if (bindedObj != null)
            collidedWith = bindedObj.name.ToString();

        if (buildable)
        {
            meshRenderer.material = canBuildMat;
            ableToBuildCheckerManager.RegisterBuildable(gameObject,buildable);
        }
        else if (!buildable)
        {
            meshRenderer.material = noBuildMat;
            ableToBuildCheckerManager.RegisterBuildable(gameObject, buildable);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("TravelPoint"))
        {
            if (other.gameObject != bindedObj)
            {
                bindedObj = other.gameObject;
                buildable = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("TravelPoint"))
        {
            if (other.gameObject != bindedObj)
            {
                bindedObj = null;
                buildable = true;
            }
        }
    }
}
