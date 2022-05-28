using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    [Tooltip("Put an empty object here, and parent all units you want to activate to that object")]
    [SerializeField] GameObject wave;
    SoldierController[] enemies;

    private void Awake()
    {
        enemies = wave.GetComponentsInChildren<SoldierController>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Friendly")) return;

        foreach(SoldierController enemy in enemies)
        {
            enemy.enabled = true;
        }
    }
}
