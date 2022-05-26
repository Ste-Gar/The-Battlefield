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
    [SerializeField] string opponentTag;
    [SerializeField] float findTargetDelay = 0.2f;
    [Tooltip("Adds a delay for searching a target at the start of the game")]
    [SerializeField] float startDelay = 1;

    Mover mover;
    Attacker attacker;

    Transform target;

    Vector3 movement;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponent<Attacker>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(FindNearestTarget), startDelay, findTargetDelay);
    }

    private void Update()
    {
        movement = Vector3.zero;

        if (target == null) return;

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
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

    private void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(opponentTag);
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                target = enemy.transform;
            }
        }
    }
}
