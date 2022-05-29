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

    public delegate void Death(GameObject thisObj);
    public static Death onDeath;

    private void OnDisable()
    {
        onDeath?.Invoke(this.gameObject);
    }

    private void Awake()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponent<Attacker>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(FindNearestTarget), Random.Range(startDelay - 0.1f,startDelay + 0.1f), findTargetDelay);
    }

    private void Update()
    {
        movement = Vector3.zero;

        if (target == null || !target.CompareTag(opponentTag)) return;
        //When a unit dies it isn't destroyed instantly. To avoid units attacking the thin air, we check the target's tag, as it is changed to "Untagged" instantly at death.

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
