using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_MoveToPoint : MonoBehaviour
{
    public Transform[] points;
    public float speed;

    private int currentPath = 0;


    // Update is called once per frame
    void Update()
    {
        if (gameObject == null || points.Length == 0 || points[points.Length - 1] == null)
        {
            Debug.LogWarning("gameObject: " + gameObject.name + " not found");
            Destroy(this);
        }
        if (transform.position == points[currentPath].position)
        {
            currentPath++;
            //if reach last point
            if (currentPath == points.Length)
            {
                currentPath = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, points[currentPath].position, speed * Time.deltaTime);
        
    }
}
