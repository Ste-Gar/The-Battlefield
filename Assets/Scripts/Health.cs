using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;

    [SerializeField] GameObject bloodFXPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();   
        }
    }

    private void Die()
    {
        Instantiate(bloodFXPrefab, transform.position, Quaternion.Euler(0, transform.eulerAngles.y + 180, 0));
        Destroy(gameObject);
    }
}
