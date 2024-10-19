using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform Asset", menuName = "Data Asset Scripting/Transform Data Asset")]
public class TransformDataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    Transform initialValue;

    [NonSerialized]
    public Transform value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}