using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{

    [SerializeField]
    Transform audioListenerTransform;

    AudioSource source;


    void Start()
    {
        source = audioListenerTransform.gameObject.AddComponent<AudioSource>();
    }


    public void PlayUISound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
