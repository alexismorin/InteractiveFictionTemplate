using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FloatEventListener : MonoBehaviour
{

    [System.Serializable]
    public class ResponseEvent : UnityEvent<float>
    {
    }

    public FloatEvent Event;
    public ResponseEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(float inputFloat)
    { Response.Invoke(inputFloat); }
}