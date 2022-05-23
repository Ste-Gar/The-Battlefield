using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] string enemyTag;
    int damage;

    private void Awake()
    {
        damage = GetComponentInParent<Attacker>().Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(enemyTag)) return;

        Health target = other.GetComponent<Health>();
        if (target == null) return;

        target.TakeDamage(damage);
    }

    public void SetEnnemyTag(string tag)
    {
        enemyTag = tag;
    }
}
