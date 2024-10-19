using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObject Data Asset", menuName = "Data Asset Scripting/GameObject Data Asset")]
public class GameObjectDataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    GameObject initialValue;

    [DebugAttribute]
    [NonSerialized]
    public GameObject value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}