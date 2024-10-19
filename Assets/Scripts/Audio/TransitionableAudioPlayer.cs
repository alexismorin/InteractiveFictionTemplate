using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionableAudioPlayer : MonoBehaviour
{
    // TODO; load area music

    [SerializeField]
    AudioClip startingClip;


    [SerializeField]
    float startingVolume = 1f;

    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;

        StartNewClip(startingClip);
    }

    public void StartNewClip(AudioClip newClip)
    {
        if (!GetComponent<AudioSource>())
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = startingClip;
            source.volume = startingVolume;
            source.loop = true;
            source.Play();
        }
    }


    void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}
