using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

[CreateAssetMenu(fileName = "String Event", menuName = "Data Asset Scripting/Events/String Event")]
public class StringEvent : ScriptableObject
{

    private List<StringEventListener> listeners =
        new List<StringEventListener>();

    public void Raise(string inputString)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inputString);
    }

    public void RegisterListener(StringEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(StringEventListener listener)
    { listeners.Remove(listener); }
}