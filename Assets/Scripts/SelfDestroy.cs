using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float timer = 4;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
