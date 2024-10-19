using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class TextAssetEventListener : MonoBehaviour
{

    [System.Serializable]
    public class ResponseEvent : UnityEvent<string>
    {
    }

    public TextAssetEvent Event;
    public ResponseEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(TextAsset textAsset)
    { Response.Invoke(textAsset.name); }
}