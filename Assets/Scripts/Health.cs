using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(string.Format("{0} has taken {1} damage.", gameObject.name, damage));
        if (health <= 0)
        {
            Die();   
        }
    }

    private void Die()
    {
        Debug.Log(string.Format("{0} is dead.", gameObject.name));
        Destroy(gameObject);
    }
}
