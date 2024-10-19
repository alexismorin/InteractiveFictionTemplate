using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PunctualText : MonoBehaviour
{

    [SerializeField]
    Vector2 horizontalClamps;
    [SerializeField]
    Vector2 verticalClamps;

    [Space(10)]

    public Vector3 worldAnchorPoint = Vector3.zero;

    // Internals

    Camera mainCamera;
    RectTransform rectTransform;
    Vector3 screenPosition;
    CanvasGroup canvasGroup;
    Coroutine fadeCoroutine = null;
    bool interrupting;

    public void Initalize(string text, Vector3 position)
    {

        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        worldAnchorPoint = position;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;

        Reposition();

        fadeCoroutine = StartCoroutine(Fade(text));
    }

    IEnumerator Fade(string text)
    {
        // Fade In

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(0f, 1f, elapsed / 1f);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Display

        float displayTime = (float)text.Length / 4f;
        yield return new WaitForSeconds(displayTime);

        // Fade Out

        elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(1f, 0f, elapsed / 1f);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        Destroy(gameObject);
    }

    IEnumerator Discard()
    {
        // Fade In

        float elapsed = 0f;
        float startOpacity = canvasGroup.alpha;

        elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(startOpacity, 0f, elapsed / 1f);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        Destroy(gameObject);
    }

    void Interrupt()
    {
        interrupting = true;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        StartCoroutine(Discard());
    }

    void Reposition()
    {

        if (!interrupting)
        {
            screenPosition = mainCamera.WorldToViewportPoint(worldAnchorPoint, Camera.MonoOrStereoscopicEye.Mono);
            screenPosition = new Vector3(Mathf.Clamp(screenPosition.x, 0f, 1f), Mathf.Clamp(screenPosition.y, 0f, 1f), Mathf.Clamp(screenPosition.z, -1f, 1f));

            if (screenPosition.z <= 0f)
            {
                Interrupt();

                //    float fixedXPosition = 1f - Mathf.Clamp01(screenPosition.x * Mathf.Abs(screenPosition.z));
                //     screenPosition = new Vector3(fixedXPosition, screenPosition.y * screenPosition.z, screenPosition.z);

            }
            else
            {
                screenPosition = new Vector3(screenPosition.x, screenPosition.y, screenPosition.z);
                rectTransform.position = new Vector3(Mathf.SmoothStep(0f, Screen.width - Screen.width * 0.4f, screenPosition.x), Mathf.SmoothStep(0f, Screen.height - Screen.width * 0.05f, screenPosition.y), 0f);
            }



        }


    }

    void LateUpdate()
    {
        Reposition();
    }
}
