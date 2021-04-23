using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class TowerRangeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Requirement: Needs a Capsule Collider to detect enemies in range
    /// 
    /// -Holds the enemies that are in range
    /// -Adjust radius of range 
    /// </summary>
    /// 
    

    [HideInInspector] public List<Transform> enemies;
    [Header("Tower Attack Behaviour Script")]
    public TowerAttackBehaviour towerAtkBehaviourScript;

    private CapsuleCollider rangeColl;
    private float rangeValue;

#pragma warning disable CS0649
    [Header("Required Component")]
    [SerializeField] private Transform rangeIndicator;
#pragma warning restore CS0649

    #region Built-in Function
    private void Start()
    {
        rangeColl = GetComponent<CapsuleCollider>();
        Transform transf = transform.parent;
        rangeValue = transform.parent.gameObject.GetComponent<TowerObject>().towerInfo.atkRange;
        SetColliderRangeRadius();
    }
    #region EnableDisable Function
    private void OnEnable()
    {
        OnUnitKilled.onEnemyKilled += OnEnemyKilled;
    }
    private void OnDisable()
    {
        OnUnitKilled.onEnemyKilled -= OnEnemyKilled;
    }
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            RegisterEnemyInList(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DeregisterEnemyInList(other.transform);
        }
    }
    #endregion
    private void SetColliderRangeRadius()
    {
        rangeColl.radius = rangeValue;
        //Set range indicator (1.5f - Ratio radius to scale)
        rangeIndicator.localScale = new Vector3(1 * rangeColl.radius * 2, rangeIndicator.localScale.y, 1 * rangeColl.radius * 2);
        //Individually times the ratio scale except for y
    }
    private void RegisterEnemyInList(Transform enemyTransform)
    {
        Debug.Log("Enemy registered : " + enemyTransform);
        enemies.Add(enemyTransform);
        towerAtkBehaviourScript.updateTargetList(enemies);
    }
    private void DeregisterEnemyInList(Transform enemyTransform)
    {
        Debug.Log("Unregister enemy");
        enemies.Remove(enemyTransform);
        towerAtkBehaviourScript.updateTargetList(enemies);
    }
    private void OnEnemyKilled(GameObject victim, GameObject killer)//Event OnUnitKilled Listener
    {
        if (victim.CompareTag("Enemy") && enemies.Contains(victim.transform))
        {
            DeregisterEnemyInList(victim.transform);
        }
    }
}
