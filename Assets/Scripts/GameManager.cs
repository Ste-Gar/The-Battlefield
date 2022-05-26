using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameObject player;
    TimeManager timeManager;
    [SerializeField] [Range(0.01f, 1)] float timescaleReduction = 0.1f;
    [SerializeField] GameObject tutorialPanel;

    private IEnumerator Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        timeManager.PauseGame();
        yield return StartCoroutine(FindObjectOfType<Fader>().FadeIn());
        timeManager.ResumeGame();
    }

    private void OnEnable()
    {
        Commander.OnDeath += StartGame;
    }

    private void OnDisable()
    {
        Commander.OnDeath -= StartGame;
    }

    void StartGame()
    {
        player.GetComponent<GhostController>().enabled = true;
        player.transform.GetChild(0).gameObject.SetActive(true);
        timeManager.SlowTime(timescaleReduction);
        tutorialPanel.SetActive(true);
    }

    public void disableTutorial()
    {
        if (tutorialPanel == null) return;
        Destroy(tutorialPanel);
    }
}
