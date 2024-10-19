using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Int Event", menuName = "Data Asset Scripting/Events/Int Event")]
public class IntEvent : ScriptableObject
{

    private List<IntEventListener> listeners =
        new List<IntEventListener>();

    public void Raise(int inputInt)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inputInt);
    }

    public void RegisterListener(IntEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(IntEventListener listener)
    { listeners.Remove(listener); }
}