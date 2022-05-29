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
        gameObject.tag = "Untagged";
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<SoldierController>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        if (TryGetComponent<Commander>(out Commander comm)) comm.enabled = false;
        Destroy(gameObject, 1);
    }
}
