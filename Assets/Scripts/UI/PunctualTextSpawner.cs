using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunctualTextSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Vector3DataAsset spawnPosition;

    [Header("Audio")]

    [SerializeField]
    AudioClipEvent audioEvent;
    [SerializeField]
    AudioClip startTextAudio;

    public void SpawnPunctualText(string text)
    {
        audioEvent.Raise(startTextAudio);
        var prefabInstance = Instantiate(prefab, spawnPosition.value, Quaternion.identity, transform);
        prefabInstance.GetComponent<PunctualText>().Initalize(text, spawnPosition.value);
    }

}
