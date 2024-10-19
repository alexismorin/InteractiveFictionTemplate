using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

[CreateAssetMenu(fileName = "Text Asset Event", menuName = "Data Asset Scripting/Events/Text Asset Event")]
public class TextAssetEvent : ScriptableObject
{

    private List<TextAssetEventListener> listeners =
        new List<TextAssetEventListener>();

    public void Raise(TextAsset dialogFile)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(dialogFile);
    }

    public void RegisterListener(TextAssetEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(TextAssetEventListener listener)
    { listeners.Remove(listener); }
}