using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    public delegate void CommanderDeath();
    public static event CommanderDeath OnDeath;

    private void OnDisable()
    {
        OnDeath.Invoke();
        GetComponent<SoundManager>().PlaySound("Kill");
    }
}
