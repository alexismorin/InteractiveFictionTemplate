using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDataAssetAssigner : MonoBehaviour
{

    [SerializeField]
    GameObjectDataAsset dialogueSystem;

    void Awake()
    {
        dialogueSystem.value = this.gameObject;
    }


}
