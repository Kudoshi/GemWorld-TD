using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeIndicator : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
