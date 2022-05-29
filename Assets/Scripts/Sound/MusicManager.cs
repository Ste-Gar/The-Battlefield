using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance { get { return instance; } }

    SoundManager soundManager;

    [SerializeField] float StartFadeInDuration = 1;
    [SerializeField] float PlayFadeOutDuration = 1;
    [SerializeField] float GameFadeInDuration = 5;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);

            soundManager = GetComponent<SoundManager>();
        }
    }

    //private void Start()
    //{
    //    //StartCoroutine(soundManager.FadeInClip("MenuMusic", StartFadeInDuration));
    //    soundManager.PlaySound("MenuMusic");
    //}

    public void PlayGameMusic()
    {
        StartCoroutine(FadeGameMusic());
    }

    private IEnumerator FadeGameMusic()
    {
        yield return StartCoroutine(soundManager.FadeOut(PlayFadeOutDuration));
        StartCoroutine(soundManager.FadeInClip("GameMusic", GameFadeInDuration));
    }
}
