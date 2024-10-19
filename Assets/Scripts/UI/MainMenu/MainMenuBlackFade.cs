using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBlackFade : MonoBehaviour
{

    [SerializeField]
    Image blackScreen;

    [SerializeField]
    AnimationCurve blackScreenAnimationCurve;

    void Start()
    {
        blackScreen.color = new Color(0f, 0f, 0f, 1f);
        AudioListener.volume = 0f;
        StartCoroutine(SmoothAnimate(1f, 0f, 2f));
    }

    public void FadeToBlack()
    {
        StartCoroutine(SmoothAnimate(0f, 1f, 2f));
    }

    IEnumerator SmoothAnimate(float start, float end, float length, float initialDelay = 0.5f)
    {
        yield return new WaitForSeconds(initialDelay);

        float elapsed = 0.0f;
        blackScreen.color = new Color(0f, 0f, 0f, start);
        while (elapsed < length)
        {
            float opacity = Mathf.SmoothStep(start, end, blackScreenAnimationCurve.Evaluate(elapsed / length));
            blackScreen.color = new Color(0f, 0f, 0f, opacity);

            AudioListener.volume = Mathf.SmoothStep(end, start, blackScreenAnimationCurve.Evaluate(elapsed / length));

            elapsed += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = new Color(0f, 0f, 0f, end);
    }
}
