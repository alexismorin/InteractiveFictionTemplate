using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{

    [Header("Look Transition")]

    [SerializeField]
    Vector3DataAsset lookTarget;

    [SerializeField]
    float lookHeightOffset = -0.75f;

    [SerializeField]
    float lookTime = 1f;
    // Internals

    Animator rig;
    Transform rigTransform;

    void Start()
    {
        rig = GetComponentInChildren<Animator>();
        rigTransform = rig.transform;
    }

    public void Animate(string animationName, bool rotateTowardsTarget)
    {

        if (rotateTowardsTarget)
        {
            StartCoroutine(TurnTowards());
        }


        AnimatorControllerParameter[] parameters = rig.parameters;
        foreach (var parameter in parameters)
        {
            if (parameter.name == animationName)
            {
                rig.ResetTrigger(animationName);
                rig.SetTrigger(animationName);
            }
        }
    }

    IEnumerator TurnTowards()
    {
        Quaternion startRotation = rigTransform.rotation;
        Quaternion goalRotation = Quaternion.LookRotation((lookTarget.value + new Vector3(0f, lookHeightOffset, 0f)) - rigTransform.position);
        float elapsed = 0.0f;

        rigTransform.rotation = startRotation;
        while (elapsed < lookTime)
        {
            rigTransform.rotation = Quaternion.Lerp(startRotation, goalRotation, Mathf.SmoothStep(0f, 1f, elapsed / lookTime));
            elapsed += Time.deltaTime;
            yield return null;
        }
        rigTransform.rotation = goalRotation;
    }
}
