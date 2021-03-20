using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
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
        chaseTargetScript= GetComponent<ChaseTarget>();

        if (chaseTargetScript == null)
        {
            Debug.Log("Chase Target Script not found on game object");
        }
    }

    public void SetProjectileStats(float projectileSpeed, float projectileDmg, Transform target, GameObject attacker)
    {
        this.projectileSpeed = projectileSpeed;
        this.projectileDmg = projectileDmg;
        this.target = target;
        this.attacker = attacker;

        StartChaseTarget();
    }

    private void StartChaseTarget() //Changes target of the chase target
    {
        chaseTargetScript.ChangeTarget(target, projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyObject>().ReceiveDamage(projectileDmg, attacker);
            Destroy(gameObject);
        }
    }
}
