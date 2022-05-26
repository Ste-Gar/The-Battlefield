using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] string firstScene;
        LevelLoader levelLoader;

        private void Awake()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }

        public void PlayGame()
        {
            levelLoader.LoadScene(firstScene);
            this.enabled = false;
        }

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }
    }
}