using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Float Event", menuName = "Data Asset Scripting/Events/Float Event")]
public class FloatEvent : ScriptableObject
{

    private List<FloatEventListener> listeners =
        new List<FloatEventListener>();

    public void Raise(float inputFloat)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inputFloat);
    }

    public void RegisterListener(FloatEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(FloatEventListener listener)
    { listeners.Remove(listener); }
}