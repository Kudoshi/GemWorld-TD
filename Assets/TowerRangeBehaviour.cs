using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public TowerAttackBehaviour towerAtkBehaviourScript;

    private CapsuleCollider rangeColl;
    private float rangeValue;

#pragma warning disable CS0649
    [SerializeField] private Transform rangeIndicator;
#pragma warning restore CS0649

    #region Built-in Function
    private void Awake()
    {
        rangeColl = GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        GetRangeValue();
        SetColliderRangeRadius();
    }
    private void Update()
    {
        //Debug function to print enemy list
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var enemy in enemies)
            {
                Debug.Log(enemy);
            }
        }

    }
    private void OnEnable()
    {
        OnUnitKilled.onEnemyKilled += OnEnemyKilled;
    }
    private void OnDisable()
    {
        OnUnitKilled.onEnemyKilled -= OnEnemyKilled;

    }
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
    private void GetRangeValue()
    {
        rangeValue = GetComponent<TowerObject>().towerInfo.atkRange;
        
    }
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
