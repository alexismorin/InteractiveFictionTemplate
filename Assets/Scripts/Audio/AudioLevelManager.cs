using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevelManager : MonoBehaviour
{

    void Start()
    {
        AudioListener.volume = 0f;

        StartCoroutine(Fade(true));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool fadeIn)
    {
        yield return null;

        if (fadeIn)
        {
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = 1f;
        }

        float elapsed = 0f;
        float fadeTime = 1f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;

            if (fadeIn)
            {
                AudioListener.volume = Mathf.SmoothStep(0f, 1f, elapsed / fadeTime);
            }
            else
            {
                AudioListener.volume = Mathf.SmoothStep(1f, 0f, elapsed / fadeTime);
            }

            yield return null;

        }

        if (fadeIn)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }

}
