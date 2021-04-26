using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AttackBehaviour_Tower : AttackBehaviour_Base
{
    public int numberOfAttacks;
    public float chargeFxTime = 0.2f;

    [Header("Attachment")]
    public ParticleSystem chargeFx;
    public GameObject projectilePf;

    private Transform attackSpawnLocation;
    public List<Transform> tempTargetList;
    public List<Transform> targetingList;
    private Coroutine[] attackOrder;
    private Tower towerInfo;
    private float attackInterval;

    private void Start()
    {
        //List initiation
        targetingList = new List<Transform>();
        for (int i = 0; i < numberOfAttacks; i++)
        {
            targetingList.Add(null);
        }
        attackOrder = new Coroutine[numberOfAttacks];

        //Class variable Initiation
        attackSpawnLocation = transform.Find("AttackSpawnLocation");
        towerInfo = GetComponent<TowerObject>().towerInfo;
        Debug.Log(towerInfo);
        //Calculate attack Interval
        attackInterval = 1 / towerInfo.atkSpeed;
    }
    public override void UpdateTargetList(List<Transform> givenTargetList)
    {
        tempTargetList = givenTargetList.ToList();

        if (tempTargetList.Count == 0)
        {
            StopAttacking();
            return;
        }

        for (int i = 0; i < numberOfAttacks; i++)
        {
            if (tempTargetList.Count == 0)
            {
                for (int x = i; x < numberOfAttacks; x++)
                {
                    targetingList[x] = null;
                }
                return;
            }

            if (tempTargetList.Contains(targetingList[i]))
            {
                Transform target = tempTargetList.First(item => item == targetingList[i]);
                tempTargetList.Remove(target);
            }
            else if (!tempTargetList.Contains(targetingList[i]))
            {
                if (attackOrder[i] != null)
                    StopCoroutine(attackOrder[i]);

                Transform target = tempTargetList[UnityEngine.Random.Range(0, tempTargetList.Count)];
                targetingList[i] = target;
                tempTargetList.Remove(target);
                Coroutine atkOrder = StartCoroutine(Attack(targetingList[i]));
                attackOrder[i] = atkOrder;
            }
        }
    }

    public IEnumerator Attack(Transform target)
    {
        while (target != null)
        {
            yield return new WaitForSeconds(chargeFxTime);
            if (target != null)
                 chargeFx.Play();
            //Spawn projectile prefab
            yield return new WaitForSeconds(attackInterval);
            GameObject projectile = Instantiate(projectilePf, attackSpawnLocation.position, projectilePf.transform.rotation);
            projectile.GetComponent<ProjectileBehaviour>().SetProjectileStats(towerInfo.atkProjectileSpeed, towerInfo.atkDamage, target, gameObject);
            yield return null;
        }
    }

    private void StopAttacking()
    {
        for (int i = 0; i < numberOfAttacks; i++)
        {
            targetingList[i] = null;
        }
        StopAllCoroutines();
    }
    // Update is called once per frame
}

