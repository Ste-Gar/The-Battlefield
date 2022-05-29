using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] float delay = 0;

    private void Start()
    {
        GetComponent<AudioSource>().PlayDelayed(delay);
    }
}
