using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] List<SoundClip> soundClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clipName)
    {
        if (audioSource.isPlaying) return;

        SoundClip soundClip = soundClips.Find(s => s.name == clipName);

        audioSource.clip = soundClip.clip;
        audioSource.volume = soundClip.volume;
        audioSource.loop = soundClip.loop;

        audioSource.Play();
    }
}
