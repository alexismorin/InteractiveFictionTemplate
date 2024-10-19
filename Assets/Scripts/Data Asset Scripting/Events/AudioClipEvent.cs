
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Clip Event", menuName = "Data Asset Scripting/Events/Audio Clip Event")]
public class AudioClipEvent : ScriptableObject
{

    private List<AudioClipEventListener> listeners =
        new List<AudioClipEventListener>();

    public void Raise(AudioClip inputAudioClip)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inputAudioClip);
    }

    public void RegisterListener(AudioClipEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(AudioClipEventListener listener)
    { listeners.Remove(listener); }
}