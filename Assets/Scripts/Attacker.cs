using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 1;
    public int Damage { get { return damage; } }
    [SerializeField] float cooldown = 0.5f;
    float currentCooldown;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        currentCooldown = cooldown;
    }

    private void Update()
    {
        currentCooldown += Time.deltaTime;
    }

    public void Attack()
    {
        if (currentCooldown >= cooldown)
        {
            animator.SetTrigger("Attack");
            currentCooldown = 0;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (gameObject.layer == collision.gameObject.layer) return;
    //    if (gameObject.CompareTag(collision.gameObject.tag)) return;

    //    Health target = collision.GetComponent<Health>();
    //    if (target == null) return;

    //    target.TakeDamage(damage);
    //}
}
