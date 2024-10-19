using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PunctualTextSender : MonoBehaviour
{
    [SerializeField]
    StringDataAsset text;

    [SerializeField]
    Vector3DataAsset positionReference;

    [SerializeField]
    StringEvent sendEvent;

    [SerializeField]
    GameEvent endInteractionEvent;

    float resetDelay = 0f;
    bool canSpawn = true;

    public void Start()
    {
        resetDelay = ((float)text.value.Length / 4f) + 3.5f;
    }

    public void Raise()
    {
        if (canSpawn)
        {
            canSpawn = false;

            if (transform.childCount == 1)
            {
                positionReference.value = transform.GetChild(0).position;
            }
            else
            {
                positionReference.value = transform.position;
            }



            sendEvent.Raise(text.value);
            Invoke("ResetSpawner", resetDelay);
        }

        endInteractionEvent.Raise();
    }

    void ResetSpawner()
    {
        canSpawn = true;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (text)
        {
            Handles.Label(transform.position + Vector3.up * 0.01f, text.name);
        }
    }
#endif
}
