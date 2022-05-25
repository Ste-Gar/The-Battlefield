using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] string firstScene;

        public void PlayGame()
        {
            SceneManager.LoadScene(firstScene);
        }

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }
    }
}