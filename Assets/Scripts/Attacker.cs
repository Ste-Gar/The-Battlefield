using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float cooldown = 0.5f;
    float currentCooldown;

    Animator weaponAnimator;

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
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
            weaponAnimator.SetTrigger("Attack");
            currentCooldown = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (gameObject.layer == collision.gameObject.layer) return;
        if (gameObject.CompareTag(collision.gameObject.tag)) return;

        Health target = collision.GetComponent<Health>();
        if (target == null) return;

        target.TakeDamage(damage);
    }
}
