using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChaseTarget))]
public class ProjectileBehaviour_Normal : MonoBehaviour
{
    //What this script does:
    //When spawn, projectile stats is set and changes the target of changetargetscript

    //Projectile stats
    public float projectileSpeed;
    public float projectileDmg;

    //Projectile Parts Needed
    [SerializeField] private Transform target;
    private ChaseTarget chaseTargetScript;
    private GameObject attacker;
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        chaseTargetScript = GetComponent<ChaseTarget>();

        if (chaseTargetScript == null)
        {
            Debug.Log("Chase Target Script not found on game object");
        }
    }
}
