using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Fader fader;

    private void Awake()
    {
        fader = FindObjectOfType<Fader>();
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(FadeAndLoadScene(scene));
    }

    private IEnumerator FadeAndLoadScene(string scene)
    {
        yield return StartCoroutine(fader.FadeOut());
        SceneManager.LoadScene(scene);
    }
}
