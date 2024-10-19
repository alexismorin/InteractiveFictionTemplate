using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class StartDialogueEventRaiser : MonoBehaviour
{


    [SerializeField]
    DialogueRunner dialogueRunner;
    [SerializeField]
    FloatDataAsset timePassedPerLine;
    [SerializeField]
    float defaultTimePassedPerLine = 0.01f;


    [SerializeField]
    GameEvent startDialogueEvent;
    [SerializeField]
    BoolDataAsset inDialogue;
    [SerializeField]
    CanvasGroup canvasGroup;

    [Header("Audio")]

    [SerializeField]
    AudioClipEvent audioEvent;
    [SerializeField]
    AudioClip startDialogueAudio;

    public void AssignDefaultTimePassing(string nodeName)
    {
        var tags = dialogueRunner.GetTagsForNode(nodeName);
        bool fulfilled = false;

        foreach (var tag in tags)
        {
            if (tag.Contains("timeScale:"))
            {
                timePassedPerLine.value = float.Parse(tag.Split(':')[1]);
                fulfilled = true;
            }
        }

        if (!fulfilled)
        {
            timePassedPerLine.value = defaultTimePassedPerLine;
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(CanvasGroupAnimator(0f, 1f, 0.2f));
// StartCoroutine(SnapPlayerCoroutine());

        inDialogue.value = true;
        audioEvent.Raise(startDialogueAudio);
        startDialogueEvent.Raise();
    }
    /*
   // Snap to animation start point
   IEnumerator SnapPlayerCoroutine()
   {

       canControlPlayer.value = false;


       Player.characterController.enabled = false;

       Vector3 startPosition = Player.playerTransform.position;
       Quaternion startRotation = Player.playerTransform.rotation;
       Vector3 goalPosition = Player.playerTransform.position;
       Quaternion goalRotation = Player.playerTransform.rotation;



       LayerMask startPointMask = LayerMask.GetMask("Player Animation Start Points");
       Collider[] nearbyColliders = Physics.OverlapSphere(Player.playerTransform.position, 5f, startPointMask, QueryTriggerInteraction.Collide);
       if (nearbyColliders.Length > 0)
       {

           goalPosition = nearbyColliders[0].transform.position;
           goalRotation = nearbyColliders[0].transform.rotation;

           float elapsed = 0.0f;
           float transitonLength = 2.5f;

           Vector3 lastPosition = Player.playerTransform.position;

           while (elapsed < transitonLength)
           {
               elapsed += Time.deltaTime;
               Player.playerTransform.position = Vector3.Lerp(startPosition, goalPosition, Mathf.SmoothStep(0f, 1f, elapsed / transitonLength));
               Player.playerTransform.rotation = Quaternion.Lerp(startRotation, goalRotation, Mathf.SmoothStep(0f, 1f, elapsed / transitonLength));

               Vector3 localVelocity = Player.playerTransform.InverseTransformVector(Player.playerTransform.position - lastPosition) / Time.deltaTime;
               Player.animator.SetFloat("velX", localVelocity.x);
               Player.animator.SetFloat("velY", localVelocity.z);

               lastPosition = Player.playerTransform.position;
               yield return null;
           }
           Player.animator.SetFloat("velX", 0f);
           Player.animator.SetFloat("velY", 0f);

           Player.playerTransform.position = goalPosition;
           Player.playerTransform.rotation = goalRotation;


       }

       Player.characterController.enabled = true;
       //   canControlPlayer.value = true;
      

} */

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
        canvasGroup.alpha = 1f;
    }


}
