using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleToBuildChecker : MonoBehaviour
{
    private Dictionary<GameObject, bool> buildCubeList;
    private bool buildable = false;
    // Start is called before the first frame update
    void Awake()
    {
        buildCubeList = new Dictionary<GameObject, bool>();
    }


    public void RegisterBuildable(GameObject obj, bool buildable)
    {
        if (!buildCubeList.ContainsKey(obj) || buildCubeList.Count == 0)
        {
            buildCubeList.Add(obj, buildable);
        }
        else
        {
            buildCubeList[obj] = buildable;
        }
        CheckIfCanBuild();
    }

    private void CheckIfCanBuild()
    {
        if (!buildCubeList.ContainsValue(false))
        {
            buildable = true;
        }
        else if (buildCubeList.ContainsValue(false))
        {
            buildable = false;
        }
    }
    public bool GetIfCanBuild()
    {
        return buildable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
