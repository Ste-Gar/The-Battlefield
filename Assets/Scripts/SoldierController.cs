using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
public class SoldierController : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 1)] float chaseSpeed = 0.5f;
    [SerializeField] float attackRange = 1;

    [Header("Find Target Parameters")]
    //[SerializeField] LayerMask opposingArmyLayer;
    [SerializeField] string opponentTag;
    [SerializeField] float circleCastRadius = 5;
    [SerializeField] float circleCastDistance = 5;

    Mover mover;
    Attacker attacker;

    Transform target;

    Vector2 movement;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponent<Attacker>();
    }

    private void Update()
    {
        FindNearestTarget();
        //FindTarget();

        movement = Vector2.zero;

        if (target == null) return;

        //RaycastHit2D nearbyEnemy = Physics2D.CircleCast(transform.position, attackRange * 2, Vector2.up, 0, opposingArmyLayer);
        //if (nearbyEnemy) 
        //{
        //    attacker.Attack();
        //}

        float distanceToTarget = Vector2.Distance(target.transform.position, transform.position);
        if (distanceToTarget > attackRange)
        {
            movement = (target.transform.position - transform.position).normalized * chaseSpeed;
        }
        else
        {
            attacker.Attack();
        }
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        mover.Move(movement);
        mover.LookAt(target.position);
    }

    //private void FindTarget()
    //{
    //    if (target != null) return;

    //    RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleCastRadius, Vector2.up, circleCastDistance, opposingArmyLayer);
    //    if (!hit)
    //    {
    //        target = null;
    //    }

    //    target = hit.transform;
    //}

    private void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(opponentTag);
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                target = enemy.transform;
            }
        }
    }
}
