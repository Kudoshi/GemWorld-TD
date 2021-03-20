using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackBehaviour : MonoBehaviour
{
    /// <summary>
    /// How the attack works:
    /// Every few second(depends on attack speed) spawns a projectile that will hit the enemy
    /// The projectile has its own behaviour which receives the target and projectile speed
    /// The projectile will chase the enemy and passing the damage info to it
    /// </summary>
    /// 



#pragma warning disable CS0649
    [Tooltip("The charging effect when the attack starts its casting animation")]
    [SerializeField] private ParticleSystem chargeFx;

    [Tooltip("The projectile prefab")]
    [SerializeField] private GameObject projectilePf;

    [Tooltip("Transform obj where projectile will spawn from")]
    [SerializeField] private Transform attackSpawnLocation;
#pragma warning restore CS0649

    private List<Transform> targetList;
    private Transform currentTarget;
    private Tower towerInfo;
    private bool shouldAttackEnemy = false;
    private float atkTimeGap;

    #region Built-In Component
    private void Start()
    {
        towerInfo = GetComponent<TowerObject>().towerInfo;
        CalculateAtkTimeGap();
        StartCoroutine(attackEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!shouldAttackEnemy)
            {
                shouldAttackEnemy = true;
            }
            else if (shouldAttackEnemy)
            {
                shouldAttackEnemy = false;
            }
        }
    }
    #endregion
    private bool loopAttack = true;
    private IEnumerator attackEnemy()
    {
        //Constantly loops over and over. If there are no enemies. ShouldAttackEnemy would be false.
        while (loopAttack)
        {
            yield return new WaitForSeconds(0.05f);
            if (shouldAttackEnemy)
            {
                chargeFx.Play();
                yield return new WaitForSeconds(atkTimeGap);
                GameObject projectile = Instantiate(projectilePf, attackSpawnLocation.position, projectilePf.transform.rotation);
                projectile.GetComponent<ProjectileBehaviour>().SetProjectileStats(towerInfo.atkProjectileSpeed, towerInfo.atkDamage, currentTarget, gameObject);
            }
        }
        
    }
    public void updateTargetList(List<Transform> targetList)
    {
        //TargetList given by towerrangebehaviour script. Copies entire targetlist over

        this.targetList = targetList;

        //Functions below does not execute if target is still in range (or not dead)
        if (targetList.Count == 0)
        {
            StopAttacking();
        }
        else if (!targetList.Contains(currentTarget)) //Executed when currentTarget(null or dead target) not in the list.
        {
            GetNewTarget();
        }
    }
    private void StopAttacking()
    {
        shouldAttackEnemy = false;
        currentTarget = null;
    }
    private void GetNewTarget()
    {

        shouldAttackEnemy = true;
        currentTarget = targetList[Random.Range(0, targetList.Count)]; //random.range int excludes the max so no need to -1
    }
    private void CalculateAtkTimeGap()
    {
        //Calculate seconds per 1 attack based on X atk per second
        //Formula : 1 sec/X attack per second = seconds per 1 attack
        atkTimeGap = 1 / towerInfo.atkSpeed;
    }
}
