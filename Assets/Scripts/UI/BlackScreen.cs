using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using TMPro;

public class BlackScreen : MonoBehaviour
{

    [SerializeField]
    Image blackScreen;

    [SerializeField]
    AnimationCurve blackScreenAnimationCurve;

    [SerializeField]
    DialogueRunner dialogueRunner;

    [Header("Day-Dependant")]

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    AnimationCurve textAnimationCurve;


    void Awake()
    {
        dialogueRunner.AddCommandHandler("FadeFromBlack", FadeFromBlack);
        dialogueRunner.AddCommandHandler("FadeToBlack", FadeToBlack);
        blackScreen.enabled = true;
        text.enabled = true;
    }

    // Called on new day
    public void NewDayFade()
    {
        blackScreen.color = new Color(0f, 0f, 0f, 1f);
        text.text = "";
        text.color = Color.clear;
        StartCoroutine(SmoothAnimate(1f, 0f, 1f, 1f));
    }

    // Called when loading mid-game
    public void MidDayFade()
    {
        blackScreen.color = new Color(0f, 0f, 0f, 1f);
        text.text = "";
        text.color = Color.clear;
        StartCoroutine(SmoothAnimate(1f, 0f, 0.5f, 1f));
    }

    public void EndDayFade()
    {
        text.text = "";
        StartCoroutine(SmoothAnimate(0f, 1f, 0.5f, 0f));
    }

    public void FadeToBlack()
    {
        StartCoroutine(SmoothAnimate(0f, 1f, 2f));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(SmoothAnimate(1f, 0f, 2f));
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

            Color lerpColor = Color.Lerp(Color.clear, Color.white, textAnimationCurve.Evaluate(elapsed / length));
            text.color = lerpColor;

            elapsed += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = new Color(0f, 0f, 0f, end);
    }

}
