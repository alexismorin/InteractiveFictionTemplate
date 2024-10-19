using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Data Asset", menuName = "Data Asset Scripting/Bool Data Asset")]
public class BoolDataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    Boolean initialValue;

    [NonSerialized]
    public Boolean value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}