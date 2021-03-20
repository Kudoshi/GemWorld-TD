using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    public Transform target;
    public float speed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        ChaseTheTarget();
    }
    public void ChangeTarget(Transform target, float speed)
    {
        this.speed = speed;
        this.target = target;
    }
    
    private void ChaseTheTarget()
    {
        if (target != null)
        {
            Vector3 dir = (target.transform.position - rb.transform.position).normalized;
            rb.MovePosition(rb.transform.position + dir * speed * Time.fixedDeltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
