using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip 
{
    public string name;
    public AudioClip clip;
    public bool loop = false;
    [Range(0, 1)] public float volume = 0.5f;
}
