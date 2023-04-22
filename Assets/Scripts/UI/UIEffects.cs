using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    [SerializeField] CanvasGroup fader;

    public IEnumerator FadeOut(float FadeSpeed)
    {
        fader.alpha = 0.0f;
        while (fader.alpha < 1f)
        {
            fader.alpha += Time.deltaTime / FadeSpeed;
            yield return null;
        }

    }

    public IEnumerator FadeIn(float FadeSpeed)
    {
        fader.alpha = 1.0f;
        while (fader.alpha > 0f)
        {
            fader.alpha -= Time.deltaTime / FadeSpeed;
            yield return null;
        }

    }
}
