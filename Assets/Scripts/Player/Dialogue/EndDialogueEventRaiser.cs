using System.Collections;
using UnityEngine;

public class EndDialogueEventRaiser : MonoBehaviour
{
    [SerializeField]
    BoolDataAsset inDialogue;
    [SerializeField]
    CanvasGroup canvasGroup;

    public void EndDialogue()
    {
        inDialogue.value = false;
 //       canControlPlayer.value = true;
        StartCoroutine(CanvasGroupAnimator(1f, 0f, 0.2f));

    }

    IEnumerator CanvasGroupAnimator(float start, float end, float length)
    {
        float elapsed = 0.0f;
        canvasGroup.alpha = start;

        while (elapsed < length)
        {
            float opacity = Mathf.SmoothStep(start, end, elapsed / length);
            canvasGroup.alpha = opacity;
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
