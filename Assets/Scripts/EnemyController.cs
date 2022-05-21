using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
public class EnemyController : MonoBehaviour
{
    //[SerializeField] float chaseRange = 3;
    [SerializeField] [Range(0.1f, 1)] float chaseSpeed = 0.5f;
    [SerializeField] float attackRange = 1;

    Mover mover;
    Attacker attacker;

    Transform player;

    Vector2 movement;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponent<Attacker>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        movement = Vector2.zero;

        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > attackRange)
        {
            movement = (player.transform.position - transform.position).normalized * chaseSpeed;
        }
        else
        {
            attacker.Attack();
        }
    }

    private void FixedUpdate()
    {
        mover.Move(movement);
        mover.LookAt(player.position);
    }
}
