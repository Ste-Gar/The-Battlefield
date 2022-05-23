using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    int damage;

    private void Awake()
    {
        damage = GetComponentInParent<Attacker>().Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(transform.root.tag)) return;

        Health target = other.GetComponent<Health>();
        if (target == null) return;

        target.TakeDamage(damage);
    }
}
