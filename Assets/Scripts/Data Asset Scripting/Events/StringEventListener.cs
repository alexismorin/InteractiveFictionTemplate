using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class StringEventListener : MonoBehaviour
{

    [System.Serializable]
    public class ResponseEvent : UnityEvent<string>
    {
    }

    public StringEvent Event;
    public ResponseEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(string inputString)
    { Response.Invoke(inputString); }
}