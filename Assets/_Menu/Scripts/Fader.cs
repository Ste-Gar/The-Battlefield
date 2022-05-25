using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1;
    Image fader;

    private void Awake()
    {
        fader = GetComponent<Image>();
    }

    public IEnumerator FadeOut()
    {
        Color curColor = fader.color;
        float duration = 0;

        while (fader.color.a < 1) 
        {
            curColor.a = Mathf.Lerp(0, 1, duration += fadeSpeed * Time.unscaledDeltaTime);
            
            fader.color = curColor;

            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        Color curColor = fader.color;
        float duration = 0;

        while (fader.color.a > 0)
        {
            curColor.a = Mathf.Lerp(1, 0, duration += fadeSpeed * Time.unscaledDeltaTime);

            fader.color = curColor;

            yield return null;
        }
    }
}
