using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    int damage;

    [SerializeField] GameObject rootObject;

    private void Awake()
    {
        damage = GetComponentInParent<Attacker>().Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(rootObject.tag)) return;

        Health target = other.GetComponent<Health>();
        if (target == null) return;

        target.TakeDamage(damage);

        PlayerController PC = rootObject.GetComponent<PlayerController>();
        if (!PC.isActiveAndEnabled) return;
        PC.PlaySound("Kill");
        FindObjectOfType<GhostController>().AddEnergy();
    }
}
