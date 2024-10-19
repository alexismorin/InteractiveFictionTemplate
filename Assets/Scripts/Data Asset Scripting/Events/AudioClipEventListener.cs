using UnityEngine;
using UnityEngine.Events;

public class AudioClipEventListener : MonoBehaviour
{

    [System.Serializable]
    public class ResponseEvent : UnityEvent<AudioClip>
    {
    }

    public AudioClipEvent Event;
    public ResponseEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(AudioClip inputAudioClip)
    { Response.Invoke(inputAudioClip); }
}