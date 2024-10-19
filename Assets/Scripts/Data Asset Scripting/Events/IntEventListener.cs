using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class IntEventListener : MonoBehaviour
{

    [System.Serializable]
    public class ResponseEvent : UnityEvent<int>
    {
    }

    public IntEvent Event;
    public ResponseEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(int inputInt)
    { Response.Invoke(inputInt); }
}