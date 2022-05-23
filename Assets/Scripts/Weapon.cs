using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] string enemyTag;
    int damage;

    BoxCollider collider;

    private void Awake()
    {
        damage = GetComponentInParent<Attacker>().Damage;

        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(enemyTag)) return;

        Health target = other.GetComponent<Health>();
        if (target == null) return;

        target.TakeDamage(damage);
    }
}
