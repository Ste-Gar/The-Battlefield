using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("UI")]
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Fader fader;

    private IEnumerator Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        //timeManager.PauseGame();
        yield return StartCoroutine(fader.FadeIn());
        //timeManager.ResumeGame();
    }

    private void OnEnable()
    {
        Commander.OnDeath += StartGame;
        GhostController.OnInsufficientEnergy += GameOver;
    }

    private void OnDisable()
    {
        Commander.OnDeath -= StartGame;
        GhostController.OnInsufficientEnergy -= GameOver;
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

    private void GameOver()
    {
        player.SetActive(false);
        StartCoroutine(fader.FadeOut());
        gameOverPanel.SetActive(true);
        timeManager.ResetTimescale();
    }

    #region UI events

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
