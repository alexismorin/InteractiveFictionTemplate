using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField]
    TextAsset yarnFile;

    [SerializeField]
    TextAssetEvent startDialogueEvent;

    void Start()
    {
        startDialogueEvent.Raise(yarnFile);
    }

   
}
